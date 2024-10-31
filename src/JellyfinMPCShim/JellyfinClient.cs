using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Channels;
using Flurl;
using Jellyfin.Sdk;
using Jellyfin.Sdk.Generated.Models;
using JellyfinMPCShim.Interfaces;
using JellyfinMPCShim.Models;
using JellyfinMPCShim.Models.WebsocketData;
using Microsoft.Extensions.Logging;
using Websocket.Client;

namespace JellyfinMPCShim;

internal class JellyfinClient : IJellyfinClient
{
    private readonly ILogger<JellyfinClient> _logger;
    private readonly JellyfinSdkSettings _jellyfinSdkSettings;
    private readonly JellyfinApiClient _jellyfinApiClient;
    private WebsocketClient? _wsc;
    private CancellationTokenSource? _keepAliveCts;
    private int _timeout = 60;
    private readonly List<IJellyfinMessageHandler> _messageHandlers = new();
    private string? _host;
    private CancellationTokenSource? _cts;

    private static readonly JsonSerializerOptions s_serializerOptions = new JsonSerializerOptions
    {
        Converters = { new JsonGuidConverter(), new JsonNullableGuidConverter() }
    };

    private readonly Channel<ResponseMessage> _queue;
    private SessionInfoDto? _session;

    public JellyfinClient(ILogger<JellyfinClient> logger, JellyfinSdkSettings jellyfinSdkSettings,
        JellyfinApiClient jellyfinApiClient)
    {
        _logger = logger;
        _jellyfinSdkSettings = jellyfinSdkSettings;
        _jellyfinApiClient = jellyfinApiClient;
        var options = new BoundedChannelOptions(25) { FullMode = BoundedChannelFullMode.Wait };
        _queue = Channel.CreateBounded<ResponseMessage>(options);
    }


    public async Task Start(string host, string username, string password, CancellationToken stoppingToken = default)
    {
        if (_session != null)
        {
            throw new InvalidOperationException("JellyfinClient already started");
        }

        _host = host;
        _cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

        _jellyfinSdkSettings.SetServerUrl(host);
        _logger.LogDebug("Connecting  to {host}", _jellyfinSdkSettings.ServerUrl);
        var systemInfo = await _jellyfinApiClient.System.Info.Public.GetAsync(cancellationToken: stoppingToken);
        _logger.LogDebug("Connected to {host} {name} {version}", _jellyfinSdkSettings.ServerUrl, systemInfo.ServerName,
            systemInfo.Version);
        var authenticationResult = await _jellyfinApiClient.Users.AuthenticateByName.PostAsync(
            new AuthenticateUserByName { Username = username, Pw = password }, cancellationToken: stoppingToken);
        _session = authenticationResult.SessionInfo;
        _jellyfinSdkSettings.SetAccessToken(authenticationResult.AccessToken);
        _logger.LogDebug("Logged in as {name} (ID {id})", authenticationResult.User.Name, authenticationResult.User.Id);
        await _jellyfinApiClient.Sessions.Capabilities.Full.PostAsync(new ClientCapabilitiesDto
        {
            PlayableMediaTypes = new List<MediaType?> { MediaType.Video },
            SupportedCommands = new List<GeneralCommandType?>
            {
                GeneralCommandType.MoveUp,
                GeneralCommandType.MoveDown,
                GeneralCommandType.MoveLeft,
                GeneralCommandType.MoveRight,
                GeneralCommandType.Select,
                GeneralCommandType.Back,
                GeneralCommandType.ToggleFullscreen,
                GeneralCommandType.GoHome,
                GeneralCommandType.GoToSettings,
                GeneralCommandType.TakeScreenshot,
                GeneralCommandType.VolumeUp,
                GeneralCommandType.VolumeDown,
                GeneralCommandType.ToggleMute,
                GeneralCommandType.SetAudioStreamIndex,
                GeneralCommandType.SetSubtitleStreamIndex,
                GeneralCommandType.Mute,
                GeneralCommandType.Unmute,
                GeneralCommandType.SetVolume,
                GeneralCommandType.DisplayContent,
                GeneralCommandType.Play,
                GeneralCommandType.PlayState,
                GeneralCommandType.PlayNext,
                GeneralCommandType.PlayMediaSource
            },
            SupportsMediaControl = true
        });

        var uri = new Url(host);
        uri.Scheme = uri.Scheme == "https" ? "wss" : "ws";
        uri.AppendPathSegment("socket").SetQueryParams(new { api_key = _jellyfinSdkSettings.AccessToken });
        _logger.LogDebug("Starting websocke client {uri}", uri);
        _wsc = new WebsocketClient(uri.ToUri());
        _wsc.MessageReceived.Subscribe(info =>
        {
            _logger.LogDebug("Websocket message received {text}", info.Text);
            _queue.Writer.WriteAsync(info, _cts.Token);
        });
        _ = Task.Run(async () =>
        {
            await HandleWebsockeMessages(_cts.Token);
        }, _cts.Token);
        await _wsc.Start();
    }

    private async Task HandleWebsockeMessages(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var info = await _queue.Reader.ReadAsync(cancellationToken);
            try
            {
                await HandleWebsockeMessage(info, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error handling websocket message {type}, {text}", info.MessageType, info.Text);
            }
        }
    }

    public async Task Stop()
    {
        if (_cts != null)
        {
            await _cts.CancelAsync();
        }

        _wsc?.Dispose();
        _wsc = null;
        try
        {
            if (_session != null)
            {
                await _jellyfinApiClient.Sessions.Logout.PostAsync();
            }
        }
        finally
        {
            _session = null;
            _jellyfinSdkSettings.SetAccessToken(null);
        }
    }

    public Task<BaseItemDto?> GetItem(Guid itemId)
    {
        _ = _session ?? throw new InvalidOperationException("Session not stated");
        return _jellyfinApiClient.Items[itemId].GetAsync();
    }

    public Task CloseTranscode(string playSessionId)
    {
        throw new NotImplementedException();
        //TODO return _hlsSegmentClient.StopEncodingProcessAsync(_sdkClientSettings.DeviceId, playSessionId);
    }

    public Task<PlaybackInfoResponse?> GetPlaybackInfo(Guid itemId)
    {
        _ = _session ?? throw new InvalidOperationException("Session not stated");
        return _jellyfinApiClient.Items[itemId].PlaybackInfo.GetAsync();
    }

    public Url GetPlaybackUrl(Guid itemId, MediaSourceInfo mediaSource)
    {
        var url = new Url(_host);
        if (mediaSource.SupportsDirectStream ?? false)
        {
            return url
                .AppendPathSegment("Videos")
                .AppendPathSegment(itemId)
                .AppendPathSegment("stream")
                .SetQueryParams(new
                {
                    @static = true, MediaSourceId = mediaSource.Id, api_key = _jellyfinSdkSettings.AccessToken
                });
        }

        throw new Exception("MediaSource doesn't support direct stream");
        //var transcodeUrl = Url.Parse(mediaSource.TranscodingUrl);
        //return url.AppendPathSegments(transcodeUrl.PathSegments).SetQueryParams(transcodeUrl.QueryParams);
    }

    public Task ReportPlaybackStopped(PlaybackStopInfo playbackStopInfo)
    {
        return _jellyfinApiClient.Sessions.Playing.Stopped.PostAsync(playbackStopInfo);
    }

    public Task ReportPlaybackStart(PlaybackStartInfo playbackStartInfo)
    {
        return _jellyfinApiClient.Sessions.Playing.PostAsync(playbackStartInfo);
    }

    public Task ReportPlaybackProgress(PlaybackProgressInfo playbackProgessInfo)
    {
        return _jellyfinApiClient.Sessions.Playing.Progress.PostAsync(playbackProgessInfo);
    }

    public void AddMessageHandler(IJellyfinMessageHandler messageHandler)
    {
        _messageHandlers.Add(messageHandler);
    }

    public void RemoveMessageHandlen(IJellyfinMessageHandler messageHandler)
    {
        _messageHandlers.Remove(messageHandler);
    }

    public Task<List<GroupInfoDto>?> SyncPlayGetGroups()
    {
        return _jellyfinApiClient.SyncPlay.List.GetAsync();
    }

    public Task SyncPlayJoinGroup(Guid groupId)
    {
        return _jellyfinApiClient.SyncPlay.Join.PostAsync(new JoinGroupRequestDto { GroupId = groupId });
    }

    public Task SyncPlayLeaveGroup()
    {
        return _jellyfinApiClient.SyncPlay.Leave.PostAsync();
    }

    public Task SyncPlayBuffering(bool isPlaying, Guid playlistItemId, long positionTicks)
    {
        return _jellyfinApiClient.SyncPlay.Buffering.PostAsync(new BufferRequestDto
        {
            IsPlaying = isPlaying,
            PlaylistItemId = playlistItemId,
            PositionTicks = positionTicks,
            When = DateTimeOffset.Now
        });
    }

    public Task SyncPlayReady(bool isPlaying, Guid playlistItemId, long positionTicks)
    {
        return _jellyfinApiClient.SyncPlay.Ready.PostAsync(new ReadyRequestDto
        {
            IsPlaying = isPlaying,
            PlaylistItemId = playlistItemId,
            PositionTicks = positionTicks,
            When = DateTimeOffset.Now
        });
    }

    public Task SyncPlayPause()
    {
        return _jellyfinApiClient.SyncPlay.Pause.PostAsync();
    }

    public Task SyncPlayUnpause()
    {
        return _jellyfinApiClient.SyncPlay.Unpause.PostAsync();
    }

    public Task SyncPlayStop()
    {
        return _jellyfinApiClient.SyncPlay.Stop.PostAsync();
    }

    public bool IsConnected { get => _session != null; }

    private async Task HandleWebsockeMessage(ResponseMessage info, CancellationToken cancellationToken)
    {
        if (info.MessageType != WebSocketMessageType.Text)
        {
            return;
        }

        var msg = JsonSerializer.Deserialize<JellyfinWebsockeMessage>(info.Text, s_serializerOptions);
        if (msg == null)
        {
            return;
        }

        _logger.LogDebug("Message {MessageId} Type {MessageType}", msg.MessageId, msg.MessageType);

        switch (msg.MessageType)
        {
            case SessionMessageType.ForceKeepAlive:
                var fkaMsg = JsonSerializer.Deserialize<JellyfinWebsockeMessage<int>>(info.Text, s_serializerOptions)!;
                _timeout = fkaMsg.Data;
                if (_keepAliveCts != null)
                {
                    await _keepAliveCts.CancelAsync();
                }

                _keepAliveCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _ = KeepAlive(_keepAliveCts.Token);
                return;
            case SessionMessageType.KeepAlive:
                return;
            case SessionMessageType.Play:
                var playMessage =
                    JsonSerializer.Deserialize<JellyfinWebsockeMessage<PlayRequest>>(info.Text, s_serializerOptions)!;
                foreach (var handler in _messageHandlers)
                {
                    try
                    {
                        await handler.HandlePlay(playMessage);
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical(e, "{messageHandler} HandlePlay failed", handler);
                    }
                }

                break;
            case SessionMessageType.Playstate:
                var playStateMessage = JsonSerializer.Deserialize<JellyfinWebsockeMessage<PlaystateRequest>>(info.Text,
                    s_serializerOptions)!;
                foreach (var handler in _messageHandlers)
                {
                    try
                    {
                        await handler.HandlePlayState(playStateMessage);
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical(e, "{messageHandler} HandlePlayState failed", handler);
                    }
                }

                break;
            case SessionMessageType.SyncPlayGroupUpdate:
                var syncPlayGroupUpdateMessage =
                    JsonSerializer.Deserialize<JellyfinWebsockeMessage<GroupUpdate>>(info.Text, s_serializerOptions)!;
                foreach (var handler in _messageHandlers)
                {
                    try
                    {
                        await handler.HandleSyncGroupUpdate(syncPlayGroupUpdateMessage);
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical(e, "{messageHandler} HandleSyncGroupUpdate failed", handler);
                    }
                }

                break;

            case SessionMessageType.SyncPlayCommand:
                var syncPlayCommandMessage =
                    JsonSerializer.Deserialize<JellyfinWebsockeMessage<SendCommand>>(info.Text, s_serializerOptions)!;
                foreach (var handler in _messageHandlers)
                {
                    try
                    {
                        await handler.HandleSyncPlayCommand(syncPlayCommandMessage);
                    }
                    catch (Exception e)
                    {
                        _logger.LogCritical(e, "{messageHandler} HandleSyncPlayCommand failed", handler);
                    }
                }

                break;
            default:
                _logger.LogDebug("Unhandled websocket message {type}, {text}", msg.MessageType, info.Text);
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
