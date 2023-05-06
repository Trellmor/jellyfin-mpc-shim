using System.Text.Json;
using Flurl;
using Jellyfin.Sdk;
using Jellyfin.Sdk.JsonConverters;
using JellyfinMPCShim.Interfaces;
using JellyfinMPCShim.Models;
using Microsoft.Extensions.Logging;
using Websocket.Client;

namespace JellyfinMPCShim;

public class JellyfinClient : IJellyfinClient
{
    private readonly ILogger<JellyfinClient> _logger;
    private readonly SdkClientSettings _sdkClientSettings;
    private readonly ISystemClient _systemClient;
    private readonly IUserClient _userClient;
    private readonly ISessionClient _sessionClient;
    private readonly IUserLibraryClient _userLibraryClient;
    private readonly IMediaInfoClient _mediaInfoClient;
    private readonly IHlsSegmentClient _hlsSegmentClient;
    private readonly IPlaystateClient _playstateClient;
    private WebsocketClient? _wsc;
    private CancellationTokenSource? _keepAliveCts;
    private int _timeout = 60;
    private SessionInfo? _session;
    private IJellyfinMessageHandler? _messageHandler;
    private string? _host;
    private CancellationTokenSource? _cts;

    public JellyfinClient(ILogger<JellyfinClient> logger, SdkClientSettings sdkClientSettings,
        ISystemClient systemClient, IUserClient userClient, ISessionClient sessionClient,
        IUserLibraryClient userLibraryClient, IMediaInfoClient mediaInfoClient, IHlsSegmentClient hlsSegmentClient,
        IPlaystateClient playstateClient)
    {
        _logger = logger;
        _sdkClientSettings = sdkClientSettings;
        _systemClient = systemClient;
        _userClient = userClient;
        _sessionClient = sessionClient;
        _userLibraryClient = userLibraryClient;
        _mediaInfoClient = mediaInfoClient;
        _hlsSegmentClient = hlsSegmentClient;
        _playstateClient = playstateClient;
    }


    public async Task Start(string host, string username, string password, CancellationToken stoppingToken = default)
    {
        _host = host;
        _cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

        _sdkClientSettings.BaseUrl = host;
        _logger.LogDebug("Connecting  to {host}", _sdkClientSettings.BaseUrl);
        var systemInfo = await _systemClient.GetPublicSystemInfoAsync(stoppingToken);
        _logger.LogDebug("Connected to {host} {name} {version}", _sdkClientSettings.BaseUrl, systemInfo.ServerName,
            systemInfo.Version);
        var authenticationResult = await _userClient.AuthenticateUserByNameAsync(
            new AuthenticateUserByName { Username = username, Pw = password },
            stoppingToken);
        _session = authenticationResult.SessionInfo;
        _sdkClientSettings.AccessToken = authenticationResult.AccessToken;
        _logger.LogDebug("Logged in as {name} (ID {id})", authenticationResult.User.Name, authenticationResult.User.Id);
        await _sessionClient.PostCapabilitiesAsync(_session.Id, new[] { "Video" },
            new[]
            {
                GeneralCommandType.MoveUp, GeneralCommandType.MoveDown, GeneralCommandType.MoveLeft,
                GeneralCommandType.MoveRight, GeneralCommandType.Select, GeneralCommandType.Back,
                GeneralCommandType.ToggleFullscreen, GeneralCommandType.GoHome, GeneralCommandType.GoToSettings,
                GeneralCommandType.TakeScreenshot, GeneralCommandType.VolumeUp, GeneralCommandType.VolumeDown,
                GeneralCommandType.ToggleMute, GeneralCommandType.SetAudioStreamIndex,
                GeneralCommandType.SetSubtitleStreamIndex, GeneralCommandType.Mute, GeneralCommandType.Unmute,
                GeneralCommandType.SetVolume, GeneralCommandType.DisplayContent, GeneralCommandType.Play,
                GeneralCommandType.PlayState, GeneralCommandType.PlayNext, GeneralCommandType.PlayMediaSource
            }, true, cancellationToken: stoppingToken);

        var uri = new Url(host);
        uri.Scheme = uri.Scheme == "https" ? "wss" : "ws";
        uri.AppendPathSegment("socket").SetQueryParams(new
        {
            api_key = _sdkClientSettings.AccessToken, device_id = _sdkClientSettings.DeviceId
        });
        _logger.LogDebug("Starting websocke client {uri}", uri);
        _wsc = new WebsocketClient(uri.ToUri());
        _wsc.MessageReceived.Subscribe(info => HandleWebsockeMessage(info, _cts.Token));
        await _wsc.Start();
    }

    public async Task Stop()
    {
        _cts?.Cancel();
        _wsc?.Dispose();
        _wsc = null;
        try
        {
            if (_session != null)
            {
                await _sessionClient.ReportSessionEndedAsync();
            }
        }
        finally
        {
            _session = null;
            _sdkClientSettings.AccessToken = null;
        }
    }

    public Task<BaseItemDto> GetItem(Guid itemId)
    {
        _ = _session ?? throw new InvalidOperationException("Session not stated");
        return _userLibraryClient.GetItemAsync(_session.UserId, itemId);
    }

    public Task CloseTranscode(string playSessionId)
    {
        return _hlsSegmentClient.StopEncodingProcessAsync(_sdkClientSettings.DeviceId, playSessionId);
    }

    public Task<PlaybackInfoResponse> GetPlaybackInfo(Guid itemId)
    {
        _ = _session ?? throw new InvalidOperationException("Session not stated");
        return _mediaInfoClient.GetPlaybackInfoAsync(itemId, _session.UserId);
    }

    public Url GetPlaybackUrl(Guid itemId, MediaSourceInfo mediaSource)
    {
        var url = new Url(_host);
        if (mediaSource.SupportsDirectStream)
        {
            return url
                .AppendPathSegment("Videos")
                .AppendPathSegment(itemId)
                .AppendPathSegment("stream")
                .SetQueryParams(new
                {
                    @static = true, MediaSourceId = mediaSource.Id, api_key = _sdkClientSettings.AccessToken
                });
        }

        throw new Exception("MediaSource doesn't support direct stream");
        //var transcodeUrl = Url.Parse(mediaSource.TranscodingUrl);
        //return url.AppendPathSegments(transcodeUrl.PathSegments).SetQueryParams(transcodeUrl.QueryParams);
    }

    public Task ReportPlaybackStopped(PlaybackStopInfo playbackStopInfo)
    {
        return _playstateClient.ReportPlaybackStoppedAsync(playbackStopInfo);
    }

    public Task ReportPlaybackStart(PlaybackStartInfo playbackStartInfo)
    {
        return _playstateClient.ReportPlaybackStartAsync(playbackStartInfo);
    }

    public Task ReportPlaybackProgress(PlaybackProgressInfo playbackProgessInfo)
    {
        return _playstateClient.ReportPlaybackProgressAsync(playbackProgessInfo);
    }

    public void SetMessageHandler(IJellyfinMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    private void HandleWebsockeMessage(ResponseMessage info, CancellationToken cancellationToken)
    {
        var serializerOptions = new JsonSerializerOptions
        {
            Converters = { new JsonGuidConverter(), new JsonNullableGuidConverter() }
        };

        _logger.LogDebug("Websocket message received {text}", info.Text);

        var msg = JsonSerializer.Deserialize<JellyfinWebsockeMessage>(info.Text, serializerOptions);
        if (msg == null)
        {
            return;
        }

        _logger.LogDebug("Message {MessageId} Type {MessageType}", msg.MessageId, msg.MessageType);

        switch (msg.MessageType)
        {
            case SessionMessageType.ForceKeepAlive:
                var fkaMsg = JsonSerializer.Deserialize<JellyfinWebsockeMessage<int>>(info.Text);
                _keepAliveCts?.Cancel();
                _keepAliveCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _ = KeepAlive(_keepAliveCts.Token);
                return;
            case SessionMessageType.KeepAlive:
                return;
            case SessionMessageType.Play:
                _messageHandler?.HandlePlay(
                    JsonSerializer.Deserialize<JellyfinWebsockeMessage<PlayRequest>>(info.Text, serializerOptions)!);
                break;
            case SessionMessageType.Playstate:
                _messageHandler?.HandlePlayState(
                    JsonSerializer.Deserialize<JellyfinWebsockeMessage<PlaystateRequest>>(info.Text,
                        serializerOptions)!);
                break;
        }
    }

    public async Task KeepAlive(CancellationToken stopToken)
    {
        while (!stopToken.IsCancellationRequested)
        {
            await Task.Delay(new TimeSpan(0, 0, _timeout / 2), stopToken);
            if (!stopToken.IsCancellationRequested && _wsc != null)
            {
                SendWebsockeMessage(new JellyfinWebsockeMessage { MessageType = SessionMessageType.KeepAlive });
            }
        }
    }

    private void SendWebsockeMessage(JellyfinWebsockeMessage message)
    {
        _wsc?.Send(JsonSerializer.Serialize(message));
    }
}
