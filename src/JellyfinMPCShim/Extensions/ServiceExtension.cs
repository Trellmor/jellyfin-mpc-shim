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
        services.AddHttpClient<ISyncPlayClient, SyncPlayClient>()
            .ConfigurePrimaryHttpMessageHandler(configureHttpHandler);
        services.AddSingleton<IJellyfinClient, JellyfinClient>();
        return services;
    }

    public static IServiceCollection AddMpcClient(this IServiceCollection services)
    {
        return AddMpcClient(services, null);
    }

    public static IServiceCollection AddMpcClient(this IServiceCollection services, Action<MpcClientOptions>? options)
    {
        if (options != null)
        {
            services.Configure(options);
        }
        else
        {
            services.AddOptions<MpcClientOptions>();
        }
        services.AddSingleton<IMpcClient, MpcClient>();
        return services;
    }
}
