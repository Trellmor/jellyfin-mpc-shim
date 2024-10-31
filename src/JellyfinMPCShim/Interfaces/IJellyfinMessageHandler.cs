using JellyfinMPCShim.Models;
using JellyfinMPCShim.Models.WebsocketData;

namespace JellyfinMPCShim.Interfaces;

public interface IJellyfinMessageHandler
{
    Task HandlePlay(JellyfinWebsockeMessage<PlayRequest> message);
    Task HandlePlayState(JellyfinWebsockeMessage<PlaystateRequest> message);
    Task HandleSyncGroupUpdate(JellyfinWebsockeMessage<GroupUpdate> syncPlayGroupUpdateMessage);
    Task HandleSyncPlayCommand(JellyfinWebsockeMessage<SendCommand> syncPlayCommandMessage);
}
