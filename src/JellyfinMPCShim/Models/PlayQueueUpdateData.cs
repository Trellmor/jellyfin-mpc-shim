namespace JellyfinMPCShim.Models;

public class PlayQueueUpdateData
{
    public string Reason { get; set; } = null!;
    public DateTime LastUpdate { get; set; }
    public List<PlaylistItem> Playlist { get; set; } = null!;
    public int PlayingItemIndex { get; set; }
    public long StartPositionTicks { get; set; }
    public bool IsPlaying { get; set; }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    //public GroupShuffleMode ShuffleMode { get; set; }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    //public GroupRepeatMode RepeatMode { get; set; }
}
