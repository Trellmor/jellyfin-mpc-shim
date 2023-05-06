namespace JellyfinMPCShim.Interfaces;

public interface IMpcClient
{
    Task Start(string path, int port);
}
