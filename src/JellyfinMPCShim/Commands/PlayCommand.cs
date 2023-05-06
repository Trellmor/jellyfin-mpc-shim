using Jellyfin.Sdk;
using JellyfinMPCShim.Models;

namespace JellyfinMPCShim.Commands;

public class PlayCommand : Command<JellyfinWebsockeMessage<PlayRequest>>{
}
