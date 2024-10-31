namespace JellyfinMPCShim.Models.WebsocketData;

public class SendCommand
{
    /// <summary>
    /// Gets the group identifier.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("GroupId")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets the playlist identifier of the playing item.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("PlaylistItemId")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public Guid PlaylistItemId { get; set; }

    /// <summary>
    /// Gets or sets the UTC time when to execute the command.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("When")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public DateTimeOffset When { get; set; }

    /// <summary>
    /// Gets the position ticks.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("PositionTicks")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public long? PositionTicks { get; set; }

    /// <summary>
    /// Gets the command.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("Command")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public SendCommandType Command { get; set; }

    /// <summary>
    /// Gets the UTC time when this command has been emitted.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("EmittedAt")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public DateTimeOffset EmittedAt { get; set; }

}

public enum SendCommandType
{

    [System.Runtime.Serialization.EnumMember(Value = @"Unpause")]
    Unpause = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"Pause")]
    Pause = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"Stop")]
    Stop = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"Seek")]
    Seek = 3,

}
