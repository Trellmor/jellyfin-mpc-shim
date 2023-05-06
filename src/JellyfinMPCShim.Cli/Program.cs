using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using Jellyfin.Sdk;
using JellyfinMPCShim.Cli;
using JellyfinMPCShim.Cli.Models;
using JellyfinMPCShim.Extensions;
using Serilog;

var version = Assembly.GetEntryAssembly()!.GetName().Version!.ToString(3);

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSerilog((provider, configuration) =>
{
    configuration
        .ReadFrom.Configuration(builder.Configuration);
});

static HttpMessageHandler DefaultHttpClientHandlerDelegate(IServiceProvider service) =>
    new SocketsHttpHandler
    {
        AutomaticDecompression = DecompressionMethods.All, RequestHeaderEncodingSelector = (_, _) => Encoding.UTF8
    };
builder.Services.AddHttpClient("Default", c =>
{
    c.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("jellyfin-mpc-shim-cli", version));
    c.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json, 1.0));
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
}).ConfigurePrimaryHttpMessageHandler(DefaultHttpClientHandlerDelegate);

var jellyfinSetting = new JellyfinSettings();
builder.Configuration.Bind("Jellyfin", jellyfinSetting);

var mpcSettings = new MpcSettings();
builder.Configuration.Bind("MPC", mpcSettings);

builder.Services.AddJellyfin(DefaultHttpClientHandlerDelegate);

builder.Services.AddMpcClient(options =>
{
    options.TempPath = Path.GetFullPath("temp");
});
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
var settings = host.Services.GetRequiredService<SdkClientSettings>();
settings.InitializeClientSettings(
    "Jellyfin MPC Shim cli",
    version,
    Environment.MachineName,
    $"jellfiin-mpc-shim-cli-{version}-14FB3D88-F151-45FA-A878-5AA6CEF3F9D2");

await host.RunAsync();
