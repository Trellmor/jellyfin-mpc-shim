using JellyfinMPCShim.Cli.Models;
using JellyfinMPCShim.Interfaces;

namespace JellyfinMPCShim.Cli;

internal class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IJellyfinClient _jellyfinClient;
    private readonly IMpcClient _mpcClient;
    private readonly JellyfinSettings _jellyfinSettings;
    private readonly MpcSettings _mpcSettings;
    private CancellationTokenSource? _cts;

    public Worker(ILogger<Worker> logger, IJellyfinClient jellyfinClient, IMpcClient mpcClient, JellyfinSettings jellyfinSettings, MpcSettings mpcSettings)
    {
        _logger = logger;
        _jellyfinClient = jellyfinClient;
        _mpcClient = mpcClient;
        _jellyfinSettings = jellyfinSettings;
        _mpcSettings = mpcSettings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
        await _jellyfinClient.Start(_jellyfinSettings.Host, _jellyfinSettings.Username, _jellyfinSettings.Password, _cts.Token);
        await _mpcClient.Start(_mpcSettings.Path, _mpcSettings.Port);
        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        finally
        {
            await _jellyfinClient.Stop();
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _cts?.Cancel();
        return base.StopAsync(cancellationToken);
    }
}
