using Flurl;
using Jellyfin.Sdk;
using Jellyfin.Sdk.Generated.Models;

namespace JellyfinMPCShim.Interfaces;

public interface IJellyfinClient
{
    Task Start(string host, string username, string password, CancellationToken stoppingToken = default);
    Task Stop();
    Task<PlaybackInfoResponse?> GetPlaybackInfo(Guid itemId);
    Url GetPlaybackUrl(Guid itemId, MediaSourceInfo mediaSource);
    Task ReportPlaybackStopped(PlaybackStopInfo playbackStopInfo);
    Task ReportPlaybackStart(PlaybackStartInfo playbackStartInfo);
    Task ReportPlaybackProgress(PlaybackProgressInfo playbackProgessInfo);
    void AddMessageHandler(IJellyfinMessageHandler messageHandler);
    Task<List<GroupInfoDto>?> SyncPlayGetGroups();
    bool IsConnected { get; }
    Task SyncPlayJoinGroup(Guid groupId);
    Task SyncPlayLeaveGroup();
    void RemoveMessageHandlen(IJellyfinMessageHandler messageHandler);
    Task SyncPlayBuffering(bool isPlaying, Guid playlistItemId, long positionTicks);
    Task SyncPlayReady(bool isPlaying, Guid playlistItemId, long positionTicks);
    Task SyncPlayPause();
    Task SyncPlayUnpause();
    Task SyncPlayStop();
}
