using System.Text.Json.Serialization;
using Jellyfin.Sdk;

namespace JellyfinMPCShim.Models;

public class JellyfinWebsockeMessage<T> : JellyfinWebsockeMessage
{
    public T Data { get; set; } = default!;
}

public class JellyfinWebsockeMessage
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SessionMessageType MessageType { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MessageId { get; set; } = null!;
}
