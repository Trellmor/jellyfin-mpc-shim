namespace JellyfinMPCShim.Models.WebsocketData;
public class PlaystateRequest
{
    /// <summary>
    /// Enum PlaystateCommand.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("Command")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public PlaystateCommand Command { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("SeekPositionTicks")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public long? SeekPositionTicks { get; set; }

    /// <summary>
    /// Gets or sets the controlling user identifier.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("ControllingUserId")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public string ControllingUserId { get; set; }

}

public enum PlaystateCommand
{

    [System.Runtime.Serialization.EnumMember(Value = @"Stop")]
    Stop = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"Pause")]
    Pause = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"Unpause")]
    Unpause = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"NextTrack")]
    NextTrack = 3,

    [System.Runtime.Serialization.EnumMember(Value = @"PreviousTrack")]
    PreviousTrack = 4,

    [System.Runtime.Serialization.EnumMember(Value = @"Seek")]
    Seek = 5,

    [System.Runtime.Serialization.EnumMember(Value = @"Rewind")]
    Rewind = 6,

    [System.Runtime.Serialization.EnumMember(Value = @"FastForward")]
    FastForward = 7,

    [System.Runtime.Serialization.EnumMember(Value = @"PlayPause")]
    PlayPause = 8,

}

