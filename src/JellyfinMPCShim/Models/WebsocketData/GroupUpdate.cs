using System.Text.Json;

namespace JellyfinMPCShim.Models.WebsocketData;

public class GroupUpdate
{
    /// <summary>
    /// Gets the group identifier.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("GroupId")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets the update type.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("Type")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public GroupUpdateType Type { get; set; }

    /// <summary>
    /// Gets the update data.
    /// </summary>

    [System.Text.Json.Serialization.JsonPropertyName("Data")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
    public object? Data { get; set; }

    public T? GetData<T>(JsonSerializerOptions? serializerOptions = null)
    {
        if (Data != null && Data is JsonElement json)
        {
            return json.Deserialize<T>(serializerOptions);
        }

        return default;
    }

}

public enum GroupUpdateType
{

    [System.Runtime.Serialization.EnumMember(Value = @"UserJoined")]
    UserJoined = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"UserLeft")]
    UserLeft = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"GroupJoined")]
    GroupJoined = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"GroupLeft")]
    GroupLeft = 3,

    [System.Runtime.Serialization.EnumMember(Value = @"StateUpdate")]
    StateUpdate = 4,

    [System.Runtime.Serialization.EnumMember(Value = @"PlayQueue")]
    PlayQueue = 5,

    [System.Runtime.Serialization.EnumMember(Value = @"NotInGroup")]
    NotInGroup = 6,

    [System.Runtime.Serialization.EnumMember(Value = @"GroupDoesNotExist")]
    GroupDoesNotExist = 7,

    [System.Runtime.Serialization.EnumMember(Value = @"CreateGroupDenied")]
    CreateGroupDenied = 8,

    [System.Runtime.Serialization.EnumMember(Value = @"JoinGroupDenied")]
    JoinGroupDenied = 9,

    [System.Runtime.Serialization.EnumMember(Value = @"LibraryAccessDenied")]
    LibraryAccessDenied = 10,

}
