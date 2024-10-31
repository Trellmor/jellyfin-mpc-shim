using System.Text.Json.Serialization;

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

public enum SessionMessageType
{
    [System.Runtime.Serialization.EnumMember(Value = @"ForceKeepAlive")]
    ForceKeepAlive = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"GeneralCommand")]
    GeneralCommand = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"UserDataChanged")]
    UserDataChanged = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"Sessions")]
    Sessions = 3,

    [System.Runtime.Serialization.EnumMember(Value = @"Play")]
    Play = 4,

    [System.Runtime.Serialization.EnumMember(Value = @"SyncPlayCommand")]
    SyncPlayCommand = 5,

    [System.Runtime.Serialization.EnumMember(Value = @"SyncPlayGroupUpdate")]
    SyncPlayGroupUpdate = 6,

    [System.Runtime.Serialization.EnumMember(Value = @"Playstate")]
    Playstate = 7,

    [System.Runtime.Serialization.EnumMember(Value = @"RestartRequired")]
    RestartRequired = 8,

    [System.Runtime.Serialization.EnumMember(Value = @"ServerShuttingDown")]
    ServerShuttingDown = 9,

    [System.Runtime.Serialization.EnumMember(Value = @"ServerRestarting")]
    ServerRestarting = 10,

    [System.Runtime.Serialization.EnumMember(Value = @"LibraryChanged")]
    LibraryChanged = 11,

    [System.Runtime.Serialization.EnumMember(Value = @"UserDeleted")]
    UserDeleted = 12,

    [System.Runtime.Serialization.EnumMember(Value = @"UserUpdated")]
    UserUpdated = 13,

    [System.Runtime.Serialization.EnumMember(Value = @"SeriesTimerCreated")]
    SeriesTimerCreated = 14,

    [System.Runtime.Serialization.EnumMember(Value = @"TimerCreated")]
    TimerCreated = 15,

    [System.Runtime.Serialization.EnumMember(Value = @"SeriesTimerCancelled")]
    SeriesTimerCancelled = 16,

    [System.Runtime.Serialization.EnumMember(Value = @"TimerCancelled")]
    TimerCancelled = 17,

    [System.Runtime.Serialization.EnumMember(Value = @"RefreshProgress")]
    RefreshProgress = 18,

    [System.Runtime.Serialization.EnumMember(Value = @"ScheduledTaskEnded")]
    ScheduledTaskEnded = 19,

    [System.Runtime.Serialization.EnumMember(Value = @"PackageInstallationCancelled")]
    PackageInstallationCancelled = 20,

    [System.Runtime.Serialization.EnumMember(Value = @"PackageInstallationFailed")]
    PackageInstallationFailed = 21,

    [System.Runtime.Serialization.EnumMember(Value = @"PackageInstallationCompleted")]
    PackageInstallationCompleted = 22,

    [System.Runtime.Serialization.EnumMember(Value = @"PackageInstalling")]
    PackageInstalling = 23,

    [System.Runtime.Serialization.EnumMember(Value = @"PackageUninstalled")]
    PackageUninstalled = 24,

    [System.Runtime.Serialization.EnumMember(Value = @"ActivityLogEntry")]
    ActivityLogEntry = 25,

    [System.Runtime.Serialization.EnumMember(Value = @"ScheduledTasksInfo")]
    ScheduledTasksInfo = 26,

    [System.Runtime.Serialization.EnumMember(Value = @"ActivityLogEntryStart")]
    ActivityLogEntryStart = 27,

    [System.Runtime.Serialization.EnumMember(Value = @"ActivityLogEntryStop")]
    ActivityLogEntryStop = 28,

    [System.Runtime.Serialization.EnumMember(Value = @"SessionsStart")]
    SessionsStart = 29,

    [System.Runtime.Serialization.EnumMember(Value = @"SessionsStop")]
    SessionsStop = 30,

    [System.Runtime.Serialization.EnumMember(Value = @"ScheduledTasksInfoStart")]
    ScheduledTasksInfoStart = 31,

    [System.Runtime.Serialization.EnumMember(Value = @"ScheduledTasksInfoStop")]
    ScheduledTasksInfoStop = 32,

    [System.Runtime.Serialization.EnumMember(Value = @"KeepAlive")]
    KeepAlive = 33,
}
