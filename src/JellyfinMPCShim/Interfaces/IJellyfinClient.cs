﻿using Flurl;
using Jellyfin.Sdk;

namespace JellyfinMPCShim.Interfaces;

public interface IJellyfinClient
{
    Task Start(string host, string username, string password, CancellationToken stoppingToken = default);
    Task Stop();
    Task<BaseItemDto> GetItem(Guid itemId);
    Task CloseTranscode(string playSessionId);
    Task<PlaybackInfoResponse> GetPlaybackInfo(Guid itemId);
    Url GetPlaybackUrl(Guid itemId, MediaSourceInfo mediaSource);
    Task ReportPlaybackStopped(PlaybackStopInfo playbackStopInfo);
    Task ReportPlaybackStart(PlaybackStartInfo playbackStartInfo);
    Task ReportPlaybackProgress(PlaybackProgressInfo playbackProgessInfo);
    void AddMessageHandler(IJellyfinMessageHandler messageHandler);
    Task<IReadOnlyList<GroupInfoDto>> SyncPlayGetGroups();
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
