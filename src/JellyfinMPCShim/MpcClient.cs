using System.Diagnostics;
using Jellyfin.Sdk;
using JellyfinMPCShim.Extensions;
using JellyfinMPCShim.Interfaces;
using JellyfinMPCShim.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MPC_HC.Domain;

namespace JellyfinMPCShim;

internal class MpcClient : IMpcClient, IJellyfinMessageHandler
{
    private readonly ILogger<MpcClient> _logger;
    private readonly IJellyfinClient _jellyfinClient;
    private readonly IOptions<MpcClientOptions> _mpcClientOptions;
    private Media? _media;
    private Info? _lastState;
    private MPCHomeCinema? _mpc;
    private MPCHomeCinemaObserver? _mpcObserver;
    private string? _path;
    private List<PlaylistItem>? _syncPlaylist;

    public MpcClient(ILogger<MpcClient> logger, IJellyfinClient jellyfinClient,
        IOptions<MpcClientOptions> mpcClientOptions)
    {
        _logger = logger;
        _jellyfinClient = jellyfinClient;
        _mpcClientOptions = mpcClientOptions;
    }

    public async Task Start(string path, int port)
    {
        _path = path;
        if (_mpcObserver != null)
        {
            _mpcObserver.Stop();
        }

        _mpc = new MPCHomeCinema($"http://localhost:{port}");
        _jellyfinClient.AddMessageHandler(this);
        await StartObserver();

        DeleteTemporaryFiles();
    }

    private void DeleteTemporaryFiles()
    {
        if (!string.IsNullOrWhiteSpace(_mpcClientOptions.Value.TempPath) &&
            Directory.Exists(_mpcClientOptions.Value.TempPath))
        {
            var files = Directory.GetFiles(_mpcClientOptions.Value.TempPath, "*.m3u");
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete {file}", file);
                }
            }
        }
    }

    private async Task StartObserver()
    {
        _mpcObserver?.Stop();
        _mpcObserver = null;

        var mpcObserver = new MPCHomeCinemaObserver(_mpc);
        mpcObserver.PropertyChanged += MpcObserverOnPropertyChanged;
        mpcObserver.Error += MpcObserverOnError;
        try
        {
            await mpcObserver.Start();
            _mpcObserver = mpcObserver;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start MPC observer");
        }
    }

    public async Task HandlePlay(JellyfinWebsockeMessage<PlayRequest> message)
    {
        //await _jellyfinClient.CloseTranscode(TODO);
        //{"ItemIds":["8fa2e080856823d65e120b0444f8fe15"],"StartPositionTicks":15694430000,"PlayCommand":"PlayNow","ControllingUserId":"1b3df32ba0ca41bda50fc19a02d5a2e1"}
        if (message.Data.PlayCommand == PlayCommand.PlayNow)
        {
            var media = new Media(_jellyfinClient, message.Data.ItemIds, message.Data.StartIndex ?? 0,
                message.Data.AudioStreamIndex, message.Data.SubtitleStreamIndex, message.Data.MediaSourceId);
            await PlayMedia(media, message.Data.StartPositionTicks ?? 0);
        }
    }

    private async Task PlayMedia(Media media, long startPosition, bool startPaused = false)
    {
        _ = _mpc ?? throw new InvalidOperationException("MpcClient has not been started");

        _media = media;
        var uri = await _media.Video.GetPlaybackUrl();

        var filename = Path.GetRandomFileName() + ".m3u";
        //Create M3U Playlist
        if (!string.IsNullOrWhiteSpace(_mpcClientOptions.Value.TempPath))
        {
            Directory.CreateDirectory(_mpcClientOptions.Value.TempPath);
            filename = Path.Combine(_mpcClientOptions.Value.TempPath, filename);
        }

        filename = Path.GetFullPath(filename);

        await using (var s = File.OpenWrite(filename))
        await using (var write = new StreamWriter(s))
        {
            await write.WriteLineAsync("#EXTM3U");
            await write.WriteAsync(uri.ToString());
        }

        try
        {
            await _mpc.GetInfo();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get MPC status");
            //Wenn MPC nicht läuft, dann starten probieren
            if (!string.IsNullOrWhiteSpace(_path) && File.Exists(_path))
            {
                _logger.LogInformation("Starting mpc {path}", _path);
                var p = Process.Start(_path);
                p.WaitForInputIdle();
                await StartObserver();
            }
        }

        try
        {
            var result = await _mpc.OpenFileAsync(filename);
            if (startPaused)
            {
                await _mpc.PauseAsync();
            }
            else
            {
                await _mpc.PlayAsync();
            }

            if (startPosition > 0)
            {
                await SetPosition(startPosition);
                var pos = TimeSpan.FromTicks(startPosition);
                pos = TimeSpan.FromSeconds(Math.Round(pos.TotalSeconds));
                result = await _mpc.SetPosition(pos);
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error communicating with MPC");
            throw;
        }
    }

    private async Task SetPosition(long startPosition)
    {
        _ = _mpc ?? throw new InvalidOperationException("MpcClient has not been started");

        var pos = TimeSpan.FromTicks(startPosition);
        pos = TimeSpan.FromSeconds(Math.Round(pos.TotalSeconds));
        if (_lastState != null)
        {
            if (pos.TotalMilliseconds > _lastState.Position.TotalMilliseconds - 5000 &&
                pos.TotalMilliseconds < _lastState.Position.TotalMilliseconds + 5000)
            {
                //Wenn die Position innerhalb von 5 sek liegt, kein Seek machen
                return;
            }
        }

        await _mpc.SetPosition(pos);
    }

    public async Task HandlePlayState(JellyfinWebsockeMessage<PlaystateRequest> message)
    {
        if (_mpc == null)
        {
            return;
        }

        try
        {
            switch (message.Data.Command)
            {
                case PlaystateCommand.Stop:
                    await _mpc.StopAsync();
                    break;
                case PlaystateCommand.Pause:
                    await _mpc.PauseAsync();
                    break;
                case PlaystateCommand.Unpause:
                    await _mpc.PlayAsync();
                    break;
                case PlaystateCommand.NextTrack:
                    break;
                case PlaystateCommand.PreviousTrack:
                    break;
                case PlaystateCommand.Seek:
                    await SetPosition(message.Data.SeekPositionTicks ?? 0);
                    break;
                case PlaystateCommand.Rewind:
                    break;
                case PlaystateCommand.FastForward:
                    break;
                case PlaystateCommand.PlayPause:
                    var info = await _mpc.GetInfo();
                    if (info.State == State.Playing)
                    {
                        await _mpc.PauseAsync();
                    }
                    else if (info.State == State.Paused)
                    {
                        await _mpc.PlayAsync();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error handling playState command");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Fatal error handling playState command");
            throw;
        }
    }

    public async Task HandleSyncGroupUpdate(JellyfinWebsockeMessage<ObjectGroupUpdate> syncPlayGroupUpdateMessage)
    {
        switch (syncPlayGroupUpdateMessage.Data.Type)
        {
            case GroupUpdateType.UserJoined:
                break;
            case GroupUpdateType.UserLeft:
                break;
            case GroupUpdateType.GroupJoined:
                _syncPlaylist = null;
                break;
            case GroupUpdateType.GroupLeft:
                _syncPlaylist = null;
                break;
            case GroupUpdateType.StateUpdate:
                break;
            case GroupUpdateType.PlayQueue:
                var data = syncPlayGroupUpdateMessage.Data.GetData<PlayQueueUpdateData>();
                if (data != null)
                {
                    _syncPlaylist = data.Playlist;
                    var media = new Media(_jellyfinClient, data.Playlist.Select(i => i.ItemId), data.PlayingItemIndex,
                        0, 0, null);
                    await PlayMedia(media, data.StartPositionTicks, !data.IsPlaying);
                }

                break;
            case GroupUpdateType.NotInGroup:
                break;
            case GroupUpdateType.GroupDoesNotExist:
                break;
            case GroupUpdateType.CreateGroupDenied:
                break;
            case GroupUpdateType.JoinGroupDenied:
                break;
            case GroupUpdateType.LibraryAccessDenied:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task HandleSyncPlayCommand(JellyfinWebsockeMessage<SendCommand> syncPlayCommandMessage)
    {
        if (_mpc == null)
        {
            return;
        }

        try
        {
            switch (syncPlayCommandMessage.Data.Command)
            {
                case SendCommandType.Unpause:
                    if (syncPlayCommandMessage.Data.PositionTicks != null)
                    {
                        await SetPosition(syncPlayCommandMessage.Data.PositionTicks ?? 0);
                    }

                    await _mpc.PlayAsync();
                    break;
                case SendCommandType.Pause:
                    await _mpc.PauseAsync();
                    if (syncPlayCommandMessage.Data.PositionTicks != null)
                    {
                        await SetPosition(syncPlayCommandMessage.Data.PositionTicks ?? 0);
                    }
                    break;
                case SendCommandType.Stop:
                    await _mpc.StopAsync();
                    break;
                case SendCommandType.Seek:
                    await SetPosition(syncPlayCommandMessage.Data.PositionTicks ?? 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error handling syncPlay command");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Fatal error handling syncPlay command");
            throw;
        }
    }

    private async void MpcObserverOnError(object sender, ExceptionEventArgs e)
    {
        var media = _media;
        _media = null;
        //Communication with MPC failed. Mark playback as stopped
        if (media != null)
        {
            await _jellyfinClient.ReportPlaybackStopped(new PlaybackStopInfo
            {
                MediaSourceId = media.Video.MediaSource?.Id,
                PlaySessionId = media.Video.PlaySessionId,
                ItemId = media.Video.Id,
                PositionTicks = (_lastState != null)
                    ? TimeSpan.FromMilliseconds(_lastState.PositionMillisec).Ticks
                    : null
            });
        }
    }

    private PlaybackProgressInfo GetProgessInfo(Info info)
    {
        _ = _media ?? throw new InvalidOperationException("No media");
        return new PlaybackProgressInfo
        {
            VolumeLevel = info.VolumeLevel,
            IsMuted = info.Muted,
            IsPaused = info.State == State.Paused,
            RepeatMode = RepeatMode.RepeatNone,
            PositionTicks = info.Position.Ticks,
            PlayMethod = PlayMethod.DirectStream,
            PlaySessionId = _media.Video.PlaySessionId,
            MediaSourceId = _media.Video.MediaSource?.Id,
            CanSeek = true,
            ItemId = _media.Video.Id
        };
    }

    private async void MpcObserverOnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (_media == null)
        {
            return;
        }

        if (await _media.Video.GetPlaybackUrl() != args.NewInfo.FilePath)
        {
            return;
        }

        _lastState = args.NewInfo;

        switch (args.Property)
        {
            case Property.File:
                break;
            case Property.State:
                switch (args.NewInfo.State)
                {
                    case State.Stoped:
                        var playbackStopInfo = new PlaybackStopInfo
                        {
                            MediaSourceId = _media.Video.MediaSource?.Id,
                            PlaySessionId = _media.Video.PlaySessionId,
                            ItemId = _media.Video.Id,
                            PositionTicks = args.OldInfo.Position.Ticks
                        };
                        _media = null;
                        await _jellyfinClient.ReportPlaybackStopped(playbackStopInfo);
                        break;
                    case State.Playing:
                        var playbackStartInfo = new PlaybackStartInfo
                        {
                            VolumeLevel = args.NewInfo.VolumeLevel,
                            IsMuted = args.NewInfo.Muted,
                            IsPaused = args.NewInfo.State == State.Paused,
                            RepeatMode = RepeatMode.RepeatNone,
                            PositionTicks = args.NewInfo.Position.Ticks,
                            PlaybackStartTimeTicks = DateTime.Now.Ticks,
                            PlayMethod = PlayMethod.DirectStream,
                            PlaySessionId = _media.Video.PlaySessionId,
                            MediaSourceId = _media.Video.MediaSource?.Id,
                            CanSeek = true,
                            ItemId = _media.Video.Id,
                        };
                        await _jellyfinClient.ReportPlaybackStart(playbackStartInfo);
                        break;
                    case State.Paused:
                        await _jellyfinClient.ReportPlaybackProgress(GetProgessInfo(args.NewInfo));
                        break;
                    case State.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            case Property.Possition:
                await _jellyfinClient.ReportPlaybackProgress(GetProgessInfo(args.NewInfo));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
