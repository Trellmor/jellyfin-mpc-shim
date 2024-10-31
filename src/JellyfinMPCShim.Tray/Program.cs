using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using Jellyfin.Sdk;
using JellyfinMPCShim.Extensions;
using JellyfinMPCShim.Tray;
using JellyfinMPCShim.Tray.Properties;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WindowsFormsLifetime;

var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3)!;


if (Settings.Default.UpgradeNeeded)
{
    Settings.Default.Upgrade();
    Settings.Default.UpgradeNeeded = false;
    Settings.Default.Save();
    Settings.Default.Reload();
}

static HttpMessageHandler DefaultHttpClientHandlerDelegate(IServiceProvider service) =>
    new SocketsHttpHandler
    {
        AutomaticDecompression = DecompressionMethods.All, RequestHeaderEncodingSelector = (_, _) => Encoding.UTF8
    };


var host = Host.CreateDefaultBuilder(args)
    .UseWindowsFormsLifetime<TrayApplicationContext, MainForm>(
        startForm => new TrayApplicationContext(startForm))
    .ConfigureServices((context, services) =>
    {
        services.AddSerilog((_, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration);
        });
        services.AddHttpClient("Default", c =>
        {
            c.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("jellyfin-mpc-shim-tray",
                version));
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json, 1.0));
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
        }).ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate);

        services.AddJellyfin();
        services.AddMpcClient(options =>
        {
            options.TempPath = Path.Combine(Path.GetTempPath(), "jellyfin-mpc-shim-tray");
        });
        services.AddSingleton<ExceptionLogger>();
    })
    .Build();

var settings = host.Services.GetRequiredService<JellyfinSdkSettings>();
settings.Initialize(
    "Jellyfin MPC Shim Tray",
    version,
    Environment.MachineName,
    $"jellfiin-mpc-shim-tray-{version}-71235034-4346-4A5F-9D82-437066B86654");
var eh = host.Services.GetService<ExceptionLogger>();
await host.RunAsync();
