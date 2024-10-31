using Jellyfin.Sdk.Generated.Sessions.Item.Playing;

namespace JellyfinMPCShim.Models.WebsocketData;
public partial class PlayRequest
{
    /// <summary>
    /// Gets or sets the item ids.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ItemIds")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public IReadOnlyList<Guid> ItemIds { get; set; }

    /// <summary>
    /// Gets or sets the start position ticks that the first item should be played at.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("StartPositionTicks")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public long? StartPositionTicks { get; set; }

    /// <summary>
    /// Gets or sets the play command.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("PlayCommand")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public PlayCommand PlayCommand { get; set; }

    /// <summary>
    /// Gets or sets the controlling user identifier.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ControllingUserId")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public Guid ControllingUserId { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("SubtitleStreamIndex")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public int? SubtitleStreamIndex { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("AudioStreamIndex")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public int? AudioStreamIndex { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("MediaSourceId")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public string MediaSourceId { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("StartIndex")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public int? StartIndex { get; set; }

}
