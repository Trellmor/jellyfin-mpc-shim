using Flurl;
using Jellyfin.Sdk;
using Jellyfin.Sdk.Generated.Models;
using JellyfinMPCShim.Interfaces;

namespace JellyfinMPCShim;

internal class Video
{
    private readonly IJellyfinClient _jellyfinClient;
    public Guid Id { get; private set; }
    private readonly int? _audioStreamIndex;
    private readonly int? _subtitleStreamIndex;
    private readonly string? _mediaSourceId;
    private string? _playbackUrl;

    public Video(IJellyfinClient jellyfinClient, Guid id, int? audioStreamIndex, int? subtitleStreamIndex,
        string? mediaSourceId = null)
    {
        _jellyfinClient = jellyfinClient;
        Id = id;
        _audioStreamIndex = audioStreamIndex;
        _subtitleStreamIndex = subtitleStreamIndex;
        _mediaSourceId = mediaSourceId;
    }

    public async Task<Url> GetPlaybackUrl()
    {
        if (_playbackUrl != null)
        {
            return _playbackUrl;
        }
        var info = await _jellyfinClient.GetPlaybackInfo(Id);
        PlaySessionId = info.PlaySessionId;

        MediaSource = GetBestSource(info.MediaSources);
        _playbackUrl = _jellyfinClient.GetPlaybackUrl(Id, MediaSource);
        return _playbackUrl;
    }

    public MediaSourceInfo? MediaSource { get; private set; }

    public string? PlaySessionId { get; private set; }

    private MediaSourceInfo GetBestSource(IReadOnlyList<MediaSourceInfo> mediaSources)
    {
        var preferred = mediaSources.FirstOrDefault(i => i.Id == _mediaSourceId);
        if (preferred != null)
        {
            return preferred;
        }

        return mediaSources.OrderByDescending(i => i.Bitrate ?? 0).First();
    }
}
