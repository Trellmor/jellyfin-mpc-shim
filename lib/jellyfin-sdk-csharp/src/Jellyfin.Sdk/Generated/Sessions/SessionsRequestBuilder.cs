// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Models;
using Jellyfin.Sdk.Generated.Sessions.Capabilities;
using Jellyfin.Sdk.Generated.Sessions.Item;
using Jellyfin.Sdk.Generated.Sessions.Logout;
using Jellyfin.Sdk.Generated.Sessions.Playing;
using Jellyfin.Sdk.Generated.Sessions.Viewing;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Jellyfin.Sdk.Generated.Sessions
{
    /// <summary>
    /// Builds and executes requests for operations under \Sessions
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SessionsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The Capabilities property</summary>
        public global::Jellyfin.Sdk.Generated.Sessions.Capabilities.CapabilitiesRequestBuilder Capabilities
        {
            get => new global::Jellyfin.Sdk.Generated.Sessions.Capabilities.CapabilitiesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Logout property</summary>
        public global::Jellyfin.Sdk.Generated.Sessions.Logout.LogoutRequestBuilder Logout
        {
            get => new global::Jellyfin.Sdk.Generated.Sessions.Logout.LogoutRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Playing property</summary>
        public global::Jellyfin.Sdk.Generated.Sessions.Playing.PlayingRequestBuilder Playing
        {
            get => new global::Jellyfin.Sdk.Generated.Sessions.Playing.PlayingRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Viewing property</summary>
        public global::Jellyfin.Sdk.Generated.Sessions.Viewing.ViewingRequestBuilder Viewing
        {
            get => new global::Jellyfin.Sdk.Generated.Sessions.Viewing.ViewingRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the Jellyfin.Sdk.Generated.Sessions.item collection</summary>
        /// <param name="position">The session id.</param>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Sessions.Item.WithSessionItemRequestBuilder"/></returns>
        public global::Jellyfin.Sdk.Generated.Sessions.Item.WithSessionItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("sessionId", position);
                return new global::Jellyfin.Sdk.Generated.Sessions.Item.WithSessionItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SessionsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Sessions{?activeWithinSeconds*,controllableByUserId*,deviceId*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SessionsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Sessions{?activeWithinSeconds*,controllableByUserId*,deviceId*}", rawUrl)
        {
        }
        /// <summary>
        /// Gets a list of sessions.
        /// </summary>
        /// <returns>A List&lt;global::Jellyfin.Sdk.Generated.Models.SessionInfoDto&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<global::Jellyfin.Sdk.Generated.Models.SessionInfoDto>?> GetAsync(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder.SessionsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<global::Jellyfin.Sdk.Generated.Models.SessionInfoDto>> GetAsync(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder.SessionsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<global::Jellyfin.Sdk.Generated.Models.SessionInfoDto>(requestInfo, global::Jellyfin.Sdk.Generated.Models.SessionInfoDto.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.AsList();
        }
        /// <summary>
        /// Gets a list of sessions.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder.SessionsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder.SessionsRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Jellyfin.Sdk.Generated.Sessions.SessionsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Gets a list of sessions.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class SessionsRequestBuilderGetQueryParameters 
        {
            /// <summary>Optional. Filter by sessions that were active in the last n seconds.</summary>
            [QueryParameter("activeWithinSeconds")]
            public int? ActiveWithinSeconds { get; set; }
            /// <summary>Filter by sessions that a given user is allowed to remote control.</summary>
            [QueryParameter("controllableByUserId")]
            public Guid? ControllableByUserId { get; set; }
            /// <summary>Filter by device Id.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("deviceId")]
            public string? DeviceId { get; set; }
#nullable restore
#else
            [QueryParameter("deviceId")]
            public string DeviceId { get; set; }
#endif
        }
    }
}
#pragma warning restore CS0618