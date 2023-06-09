﻿using Jellyfin.Sdk;
using JellyfinMPCShim.Models;

namespace JellyfinMPCShim.Interfaces;

public interface IJellyfinMessageHandler
{
    Task HandlePlay(JellyfinWebsockeMessage<PlayRequest> message);
    Task HandlePlayState(JellyfinWebsockeMessage<PlaystateRequest> message);
    Task HandleSyncGroupUpdate(JellyfinWebsockeMessage<ObjectGroupUpdate> syncPlayGroupUpdateMessage);
    Task HandleSyncPlayCommand(JellyfinWebsockeMessage<SendCommand> syncPlayCommandMessage);
}
