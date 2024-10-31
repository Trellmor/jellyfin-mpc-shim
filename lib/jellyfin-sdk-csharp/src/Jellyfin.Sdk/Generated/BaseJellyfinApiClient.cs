// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Albums;
using Jellyfin.Sdk.Generated.Artists;
using Jellyfin.Sdk.Generated.Audio;
using Jellyfin.Sdk.Generated.Auth;
using Jellyfin.Sdk.Generated.Branding;
using Jellyfin.Sdk.Generated.Channels;
using Jellyfin.Sdk.Generated.ClientLog;
using Jellyfin.Sdk.Generated.Collections;
using Jellyfin.Sdk.Generated.Devices;
using Jellyfin.Sdk.Generated.DisplayPreferences;
using Jellyfin.Sdk.Generated.EnvironmentNamespace;
using Jellyfin.Sdk.Generated.FallbackFont;
using Jellyfin.Sdk.Generated.Genres;
using Jellyfin.Sdk.Generated.GetUtcTime;
using Jellyfin.Sdk.Generated.Items;
using Jellyfin.Sdk.Generated.Libraries;
using Jellyfin.Sdk.Generated.Library;
using Jellyfin.Sdk.Generated.LiveStreams;
using Jellyfin.Sdk.Generated.LiveTv;
using Jellyfin.Sdk.Generated.Localization;
using Jellyfin.Sdk.Generated.MediaSegments;
using Jellyfin.Sdk.Generated.Movies;
using Jellyfin.Sdk.Generated.MusicGenres;
using Jellyfin.Sdk.Generated.Packages;
using Jellyfin.Sdk.Generated.Persons;
using Jellyfin.Sdk.Generated.Playback;
using Jellyfin.Sdk.Generated.PlayingItems;
using Jellyfin.Sdk.Generated.Playlists;
using Jellyfin.Sdk.Generated.Plugins;
using Jellyfin.Sdk.Generated.Providers;
using Jellyfin.Sdk.Generated.QuickConnect;
using Jellyfin.Sdk.Generated.Repositories;
using Jellyfin.Sdk.Generated.ScheduledTasks;
using Jellyfin.Sdk.Generated.Search;
using Jellyfin.Sdk.Generated.Sessions;
using Jellyfin.Sdk.Generated.Shows;
using Jellyfin.Sdk.Generated.Songs;
using Jellyfin.Sdk.Generated.Startup;
using Jellyfin.Sdk.Generated.Studios;
using Jellyfin.Sdk.Generated.SyncPlay;
using Jellyfin.Sdk.Generated.System;
using Jellyfin.Sdk.Generated.Tmdb;
using Jellyfin.Sdk.Generated.Trailers;
using Jellyfin.Sdk.Generated.UserFavoriteItems;
using Jellyfin.Sdk.Generated.UserImage;
using Jellyfin.Sdk.Generated.UserItems;
using Jellyfin.Sdk.Generated.UserPlayedItems;
using Jellyfin.Sdk.Generated.UserViews;
using Jellyfin.Sdk.Generated.Users;
using Jellyfin.Sdk.Generated.Videos;
using Jellyfin.Sdk.Generated.Web;
using Jellyfin.Sdk.Generated.Years;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Serialization.Form;
using Microsoft.Kiota.Serialization.Json;
using Microsoft.Kiota.Serialization.Multipart;
using Microsoft.Kiota.Serialization.Text;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated
{
    /// <summary>
    /// The main entry point of the SDK, exposes the configuration and the fluent API.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class BaseJellyfinApiClient : BaseRequestBuilder
    {
        /// <summary>The Albums property</summary>
        public global::Jellyfin.Sdk.Generated.Albums.AlbumsRequestBuilder Albums
        {
            get => new global::Jellyfin.Sdk.Generated.Albums.AlbumsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Artists property</summary>
        public global::Jellyfin.Sdk.Generated.Artists.ArtistsRequestBuilder Artists
        {
            get => new global::Jellyfin.Sdk.Generated.Artists.ArtistsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Audio property</summary>
        public global::Jellyfin.Sdk.Generated.Audio.AudioRequestBuilder Audio
        {
            get => new global::Jellyfin.Sdk.Generated.Audio.AudioRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Auth property</summary>
        public global::Jellyfin.Sdk.Generated.Auth.AuthRequestBuilder Auth
        {
            get => new global::Jellyfin.Sdk.Generated.Auth.AuthRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Branding property</summary>
        public global::Jellyfin.Sdk.Generated.Branding.BrandingRequestBuilder Branding
        {
            get => new global::Jellyfin.Sdk.Generated.Branding.BrandingRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Channels property</summary>
        public global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder Channels
        {
            get => new global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The ClientLog property</summary>
        public global::Jellyfin.Sdk.Generated.ClientLog.ClientLogRequestBuilder ClientLog
        {
            get => new global::Jellyfin.Sdk.Generated.ClientLog.ClientLogRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Collections property</summary>
        public global::Jellyfin.Sdk.Generated.Collections.CollectionsRequestBuilder Collections
        {
            get => new global::Jellyfin.Sdk.Generated.Collections.CollectionsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Devices property</summary>
        public global::Jellyfin.Sdk.Generated.Devices.DevicesRequestBuilder Devices
        {
            get => new global::Jellyfin.Sdk.Generated.Devices.DevicesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The DisplayPreferences property</summary>
        public global::Jellyfin.Sdk.Generated.DisplayPreferences.DisplayPreferencesRequestBuilder DisplayPreferences
        {
            get => new global::Jellyfin.Sdk.Generated.DisplayPreferences.DisplayPreferencesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Environment property</summary>
        public global::Jellyfin.Sdk.Generated.EnvironmentNamespace.EnvironmentRequestBuilder Environment
        {
            get => new global::Jellyfin.Sdk.Generated.EnvironmentNamespace.EnvironmentRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The FallbackFont property</summary>
        public global::Jellyfin.Sdk.Generated.FallbackFont.FallbackFontRequestBuilder FallbackFont
        {
            get => new global::Jellyfin.Sdk.Generated.FallbackFont.FallbackFontRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Genres property</summary>
        public global::Jellyfin.Sdk.Generated.Genres.GenresRequestBuilder Genres
        {
            get => new global::Jellyfin.Sdk.Generated.Genres.GenresRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The GetUtcTime property</summary>
        public global::Jellyfin.Sdk.Generated.GetUtcTime.GetUtcTimeRequestBuilder GetUtcTime
        {
            get => new global::Jellyfin.Sdk.Generated.GetUtcTime.GetUtcTimeRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Items property</summary>
        public global::Jellyfin.Sdk.Generated.Items.ItemsRequestBuilder Items
        {
            get => new global::Jellyfin.Sdk.Generated.Items.ItemsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Libraries property</summary>
        public global::Jellyfin.Sdk.Generated.Libraries.LibrariesRequestBuilder Libraries
        {
            get => new global::Jellyfin.Sdk.Generated.Libraries.LibrariesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Library property</summary>
        public global::Jellyfin.Sdk.Generated.Library.LibraryRequestBuilder Library
        {
            get => new global::Jellyfin.Sdk.Generated.Library.LibraryRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The LiveStreams property</summary>
        public global::Jellyfin.Sdk.Generated.LiveStreams.LiveStreamsRequestBuilder LiveStreams
        {
            get => new global::Jellyfin.Sdk.Generated.LiveStreams.LiveStreamsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The LiveTv property</summary>
        public global::Jellyfin.Sdk.Generated.LiveTv.LiveTvRequestBuilder LiveTv
        {
            get => new global::Jellyfin.Sdk.Generated.LiveTv.LiveTvRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Localization property</summary>
        public global::Jellyfin.Sdk.Generated.Localization.LocalizationRequestBuilder Localization
        {
            get => new global::Jellyfin.Sdk.Generated.Localization.LocalizationRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The MediaSegments property</summary>
        public global::Jellyfin.Sdk.Generated.MediaSegments.MediaSegmentsRequestBuilder MediaSegments
        {
            get => new global::Jellyfin.Sdk.Generated.MediaSegments.MediaSegmentsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Movies property</summary>
        public global::Jellyfin.Sdk.Generated.Movies.MoviesRequestBuilder Movies
        {
            get => new global::Jellyfin.Sdk.Generated.Movies.MoviesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The MusicGenres property</summary>
        public global::Jellyfin.Sdk.Generated.MusicGenres.MusicGenresRequestBuilder MusicGenres
        {
            get => new global::Jellyfin.Sdk.Generated.MusicGenres.MusicGenresRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Packages property</summary>
        public global::Jellyfin.Sdk.Generated.Packages.PackagesRequestBuilder Packages
        {
            get => new global::Jellyfin.Sdk.Generated.Packages.PackagesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Persons property</summary>
        public global::Jellyfin.Sdk.Generated.Persons.PersonsRequestBuilder Persons
        {
            get => new global::Jellyfin.Sdk.Generated.Persons.PersonsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Playback property</summary>
        public global::Jellyfin.Sdk.Generated.Playback.PlaybackRequestBuilder Playback
        {
            get => new global::Jellyfin.Sdk.Generated.Playback.PlaybackRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The PlayingItems property</summary>
        public global::Jellyfin.Sdk.Generated.PlayingItems.PlayingItemsRequestBuilder PlayingItems
        {
            get => new global::Jellyfin.Sdk.Generated.PlayingItems.PlayingItemsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Playlists property</summary>
        public global::Jellyfin.Sdk.Generated.Playlists.PlaylistsRequestBuilder Playlists
        {
            get => new global::Jellyfin.Sdk.Generated.Playlists.PlaylistsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Plugins property</summary>
        public global::Jellyfin.Sdk.Generated.Plugins.PluginsRequestBuilder Plugins
        {
            get => new global::Jellyfin.Sdk.Generated.Plugins.PluginsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Providers property</summary>
        public global::Jellyfin.Sdk.Generated.Providers.ProvidersRequestBuilder Providers
        {
            get => new global::Jellyfin.Sdk.Generated.Providers.ProvidersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The QuickConnect property</summary>
        public global::Jellyfin.Sdk.Generated.QuickConnect.QuickConnectRequestBuilder QuickConnect
        {
            get => new global::Jellyfin.Sdk.Generated.QuickConnect.QuickConnectRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Repositories property</summary>
        public global::Jellyfin.Sdk.Generated.Repositories.RepositoriesRequestBuilder Repositories
        {
            get => new global::Jellyfin.Sdk.Generated.Repositories.RepositoriesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The ScheduledTasks property</summary>
        public global::Jellyfin.Sdk.Generated.ScheduledTasks.ScheduledTasksRequestBuilder ScheduledTasks
        {
            get => new global::Jellyfin.Sdk.Generated.ScheduledTasks.ScheduledTasksRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Search property</summary>
        public global::Jellyfin.Sdk.Generated.Search.SearchRequestBuilder Search
        {
            get => new global::Jellyfin.Sdk.Generated.Search.SearchRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Sessions property</summary>
        public global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder Sessions
        {
            get => new global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Shows property</summary>
        public global::Jellyfin.Sdk.Generated.Shows.ShowsRequestBuilder Shows
        {
            get => new global::Jellyfin.Sdk.Generated.Shows.ShowsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Songs property</summary>
        public global::Jellyfin.Sdk.Generated.Songs.SongsRequestBuilder Songs
        {
            get => new global::Jellyfin.Sdk.Generated.Songs.SongsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Startup property</summary>
        public global::Jellyfin.Sdk.Generated.Startup.StartupRequestBuilder Startup
        {
            get => new global::Jellyfin.Sdk.Generated.Startup.StartupRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Studios property</summary>
        public global::Jellyfin.Sdk.Generated.Studios.StudiosRequestBuilder Studios
        {
            get => new global::Jellyfin.Sdk.Generated.Studios.StudiosRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The SyncPlay property</summary>
        public global::Jellyfin.Sdk.Generated.SyncPlay.SyncPlayRequestBuilder SyncPlay
        {
            get => new global::Jellyfin.Sdk.Generated.SyncPlay.SyncPlayRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The System property</summary>
        public global::Jellyfin.Sdk.Generated.System.SystemRequestBuilder System
        {
            get => new global::Jellyfin.Sdk.Generated.System.SystemRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Tmdb property</summary>
        public global::Jellyfin.Sdk.Generated.Tmdb.TmdbRequestBuilder Tmdb
        {
            get => new global::Jellyfin.Sdk.Generated.Tmdb.TmdbRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Trailers property</summary>
        public global::Jellyfin.Sdk.Generated.Trailers.TrailersRequestBuilder Trailers
        {
            get => new global::Jellyfin.Sdk.Generated.Trailers.TrailersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The UserFavoriteItems property</summary>
        public global::Jellyfin.Sdk.Generated.UserFavoriteItems.UserFavoriteItemsRequestBuilder UserFavoriteItems
        {
            get => new global::Jellyfin.Sdk.Generated.UserFavoriteItems.UserFavoriteItemsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The UserImage property</summary>
        public global::Jellyfin.Sdk.Generated.UserImage.UserImageRequestBuilder UserImage
        {
            get => new global::Jellyfin.Sdk.Generated.UserImage.UserImageRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The UserItems property</summary>
        public global::Jellyfin.Sdk.Generated.UserItems.UserItemsRequestBuilder UserItems
        {
            get => new global::Jellyfin.Sdk.Generated.UserItems.UserItemsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The UserPlayedItems property</summary>
        public global::Jellyfin.Sdk.Generated.UserPlayedItems.UserPlayedItemsRequestBuilder UserPlayedItems
        {
            get => new global::Jellyfin.Sdk.Generated.UserPlayedItems.UserPlayedItemsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Users property</summary>
        public global::Jellyfin.Sdk.Generated.Users.UsersRequestBuilder Users
        {
            get => new global::Jellyfin.Sdk.Generated.Users.UsersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The UserViews property</summary>
        public global::Jellyfin.Sdk.Generated.UserViews.UserViewsRequestBuilder UserViews
        {
            get => new global::Jellyfin.Sdk.Generated.UserViews.UserViewsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Videos property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.VideosRequestBuilder Videos
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.VideosRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The web property</summary>
        public global::Jellyfin.Sdk.Generated.Web.WebRequestBuilder Web
        {
            get => new global::Jellyfin.Sdk.Generated.Web.WebRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Years property</summary>
        public global::Jellyfin.Sdk.Generated.Years.YearsRequestBuilder Years
        {
            get => new global::Jellyfin.Sdk.Generated.Years.YearsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.BaseJellyfinApiClient"/> and sets the default values.
        /// </summary>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public BaseJellyfinApiClient(IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}", new Dictionary<string, object>())
        {
            ApiClientBuilder.RegisterDefaultSerializer<JsonSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<TextSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<FormSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<MultipartSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<JsonParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<TextParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<FormParseNodeFactory>();
            if (string.IsNullOrEmpty(RequestAdapter.BaseUrl))
            {
                RequestAdapter.BaseUrl = "http://localhost";
            }
            PathParameters.TryAdd("baseurl", RequestAdapter.BaseUrl);
        }
    }
}
#pragma warning restore CS0618