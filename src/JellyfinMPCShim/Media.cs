using JellyfinMPCShim.Interfaces;

namespace JellyfinMPCShim;

internal class Media
{
    private readonly IJellyfinClient _jellyfinClient;
    private readonly List<Guid> _itemIds;
    private readonly int _itemIndex;

    public Media(IJellyfinClient jellyfinClient, IEnumerable<Guid> itemIds, int itemIndex, int? audioStreamIndex,
        int? subtitleStreamIndex, string? mediaSourceId)
    {
        _jellyfinClient = jellyfinClient;
        _itemIds = itemIds.ToList();
        _itemIndex = itemIndex;
        Video = new Video(jellyfinClient, _itemIds[itemIndex], audioStreamIndex, subtitleStreamIndex, mediaSourceId);
    }

    public Video Video { get; private set; }
}
