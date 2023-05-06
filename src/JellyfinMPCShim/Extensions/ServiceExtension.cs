using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Jellyfin.Sdk;
using JellyfinMPCShim.Interfaces;
using JellyfinMPCShim.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JellyfinMPCShim.Extensions;

public static class ServiceExtension
{

    public static IServiceCollection AddJellyfin(this IServiceCollection services, Func<IServiceProvider, HttpMessageHandler> configureHttpHandler)
    {
        services.AddSingleton<SdkClientSettings>();
        services.AddHttpClient<ISystemClient, SystemClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddHttpClient<IUserClient, UserClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddHttpClient<ISessionClient, SessionClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddHttpClient<IUserLibraryClient, UserLibraryClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddHttpClient<IMediaInfoClient, MediaInfoClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddHttpClient<IHlsSegmentClient, HlsSegmentClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddHttpClient<IPlaystateClient, PlaystateClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddSingleton<IJellyfinClient, JellyfinClient>();
        return services;
    }

    public static IServiceCollection AddMpcClient(this IServiceCollection services)
    {
        services.AddSingleton<IMpcClient, MpcClient>();
        return services;
    }
}
