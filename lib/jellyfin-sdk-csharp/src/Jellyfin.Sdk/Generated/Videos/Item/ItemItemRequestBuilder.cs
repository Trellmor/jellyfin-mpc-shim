// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Videos.Item.AdditionalParts;
using Jellyfin.Sdk.Generated.Videos.Item.AlternateSources;
using Jellyfin.Sdk.Generated.Videos.Item.Hls1;
using Jellyfin.Sdk.Generated.Videos.Item.Hls;
using Jellyfin.Sdk.Generated.Videos.Item.Item;
using Jellyfin.Sdk.Generated.Videos.Item.LiveM3u8;
using Jellyfin.Sdk.Generated.Videos.Item.MainM3u8;
using Jellyfin.Sdk.Generated.Videos.Item.MasterM3u8;
using Jellyfin.Sdk.Generated.Videos.Item.StreamNamespace;
using Jellyfin.Sdk.Generated.Videos.Item.StreamWithContainer;
using Jellyfin.Sdk.Generated.Videos.Item.Subtitles;
using Jellyfin.Sdk.Generated.Videos.Item.Trickplay;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated.Videos.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \Videos\{item-id}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ItemItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The AdditionalParts property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.AdditionalParts.AdditionalPartsRequestBuilder AdditionalParts
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.AdditionalParts.AdditionalPartsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The AlternateSources property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.AlternateSources.AlternateSourcesRequestBuilder AlternateSources
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.AlternateSources.AlternateSourcesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The hls property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.Hls.HlsRequestBuilder Hls
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.Hls.HlsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The hls1 property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.Hls1.Hls1RequestBuilder Hls1
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.Hls1.Hls1RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The liveM3u8 property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.LiveM3u8.LiveM3u8RequestBuilder LiveM3u8
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.LiveM3u8.LiveM3u8RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The mainM3u8 property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.MainM3u8.MainM3u8RequestBuilder MainM3u8
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.MainM3u8.MainM3u8RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The masterM3u8 property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.MasterM3u8.MasterM3u8RequestBuilder MasterM3u8
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.MasterM3u8.MasterM3u8RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The stream property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.StreamNamespace.StreamRequestBuilder Stream
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.StreamNamespace.StreamRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Subtitles property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.Subtitles.SubtitlesRequestBuilder Subtitles
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.Subtitles.SubtitlesRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Trickplay property</summary>
        public global::Jellyfin.Sdk.Generated.Videos.Item.Trickplay.TrickplayRequestBuilder Trickplay
        {
            get => new global::Jellyfin.Sdk.Generated.Videos.Item.Trickplay.TrickplayRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the Jellyfin.Sdk.Generated.Videos.item.item collection</summary>
        /// <param name="position">The media source id.</param>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Videos.Item.Item.MediaSourceItemRequestBuilder"/></returns>
        public global::Jellyfin.Sdk.Generated.Videos.Item.Item.MediaSourceItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("mediaSource%2Did", position);
                return new global::Jellyfin.Sdk.Generated.Videos.Item.Item.MediaSourceItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Videos.Item.ItemItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ItemItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Videos/{item%2Did}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Videos.Item.ItemItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ItemItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Videos/{item%2Did}", rawUrl)
        {
        }
        /// <summary>
        /// Builds and executes requests for operations under \Videos\{item-id}\stream.{container}
        /// </summary>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Videos.Item.StreamWithContainer.StreamWithContainerRequestBuilder"/></returns>
        /// <param name="container">The video container. Possible values are: ts, webm, asf, wmv, ogv, mp4, m4v, mkv, mpeg, mpg, avi, 3gp, wmv, wtv, m2ts, mov, iso, flv.</param>
        public global::Jellyfin.Sdk.Generated.Videos.Item.StreamWithContainer.StreamWithContainerRequestBuilder StreamWithContainer(string container)
        {
            if(string.IsNullOrEmpty(container)) throw new ArgumentNullException(nameof(container));
            return new global::Jellyfin.Sdk.Generated.Videos.Item.StreamWithContainer.StreamWithContainerRequestBuilder(PathParameters, RequestAdapter, container);
        }
    }
}
#pragma warning restore CS0618