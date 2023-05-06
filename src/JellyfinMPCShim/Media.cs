using JellyfinMPCShim.Interfaces;

namespace JellyfinMPCShim;

internal class Media
{
    private readonly IJellyfinClient _jellyfinClient;
    private readonly IReadOnlyList<Guid> _itemIds;
    private readonly int _itemIndex;

    public Media(IJellyfinClient jellyfinClient, IReadOnlyList<Guid> itemIds, int itemIndex, int? audioStreamIndex,
        int? subtitleStreamIndex, string mediaSourceId)
    {
        _jellyfinClient = jellyfinClient;
        _itemIds = itemIds;
        _itemIndex = itemIndex;
        Video = new Video(jellyfinClient, _itemIds[itemIndex], audioStreamIndex, subtitleStreamIndex, mediaSourceId);
    }

    public Video Video { get; private set; }
}
