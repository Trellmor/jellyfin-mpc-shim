// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Channels.Features;
using Jellyfin.Sdk.Generated.Channels.Item;
using Jellyfin.Sdk.Generated.Channels.Items;
using Jellyfin.Sdk.Generated.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Jellyfin.Sdk.Generated.Channels
{
    /// <summary>
    /// Builds and executes requests for operations under \Channels
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ChannelsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The Features property</summary>
        public global::Jellyfin.Sdk.Generated.Channels.Features.FeaturesRequestBuilder Features
        {
            get => new global::Jellyfin.Sdk.Generated.Channels.Features.FeaturesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Items property</summary>
        public global::Jellyfin.Sdk.Generated.Channels.Items.ItemsRequestBuilder Items
        {
            get => new global::Jellyfin.Sdk.Generated.Channels.Items.ItemsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the Jellyfin.Sdk.Generated.Channels.item collection</summary>
        /// <param name="position">Channel id.</param>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Channels.Item.WithChannelItemRequestBuilder"/></returns>
        public global::Jellyfin.Sdk.Generated.Channels.Item.WithChannelItemRequestBuilder this[Guid position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("channelId", position);
                return new global::Jellyfin.Sdk.Generated.Channels.Item.WithChannelItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ChannelsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Channels{?isFavorite*,limit*,startIndex*,supportsLatestItems*,supportsMediaDeletion*,userId*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ChannelsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Channels{?isFavorite*,limit*,startIndex*,supportsLatestItems*,supportsMediaDeletion*,userId*}", rawUrl)
        {
        }
        /// <summary>
        /// Gets available channels.
        /// </summary>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Models.BaseItemDtoQueryResult"/></returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<global::Jellyfin.Sdk.Generated.Models.BaseItemDtoQueryResult?> GetAsync(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder.ChannelsRequestBuilderGetQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<global::Jellyfin.Sdk.Generated.Models.BaseItemDtoQueryResult> GetAsync(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder.ChannelsRequestBuilderGetQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            return await RequestAdapter.SendAsync<global::Jellyfin.Sdk.Generated.Models.BaseItemDtoQueryResult>(requestInfo, global::Jellyfin.Sdk.Generated.Models.BaseItemDtoQueryResult.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Gets available channels.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder.ChannelsRequestBuilderGetQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder.ChannelsRequestBuilderGetQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json, application/json;profile=\"CamelCase\", application/json;profile=\"PascalCase\"");
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Jellyfin.Sdk.Generated.Channels.ChannelsRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Gets available channels.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class ChannelsRequestBuilderGetQueryParameters 
        {
            /// <summary>Optional. Filter by channels that are favorite.</summary>
            [QueryParameter("isFavorite")]
            public bool? IsFavorite { get; set; }
            /// <summary>Optional. The maximum number of records to return.</summary>
            [QueryParameter("limit")]
            public int? Limit { get; set; }
            /// <summary>Optional. The record index to start at. All items with a lower index will be dropped from the results.</summary>
            [QueryParameter("startIndex")]
            public int? StartIndex { get; set; }
            /// <summary>Optional. Filter by channels that support getting latest items.</summary>
            [QueryParameter("supportsLatestItems")]
            public bool? SupportsLatestItems { get; set; }
            /// <summary>Optional. Filter by channels that support media deletion.</summary>
            [QueryParameter("supportsMediaDeletion")]
            public bool? SupportsMediaDeletion { get; set; }
            /// <summary>User Id to filter by. Use System.Guid.Empty to not filter by user.</summary>
            [QueryParameter("userId")]
            public Guid? UserId { get; set; }
        }
    }
}
#pragma warning restore CS0618