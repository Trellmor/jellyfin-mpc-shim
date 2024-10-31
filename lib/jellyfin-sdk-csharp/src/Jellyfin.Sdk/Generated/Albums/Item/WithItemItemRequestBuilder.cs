// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Albums.Item.InstantMix;
using Jellyfin.Sdk.Generated.Albums.Item.Similar;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated.Albums.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \Albums\{itemId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithItemItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The InstantMix property</summary>
        public global::Jellyfin.Sdk.Generated.Albums.Item.InstantMix.InstantMixRequestBuilder InstantMix
        {
            get => new global::Jellyfin.Sdk.Generated.Albums.Item.InstantMix.InstantMixRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Similar property</summary>
        public global::Jellyfin.Sdk.Generated.Albums.Item.Similar.SimilarRequestBuilder Similar
        {
            get => new global::Jellyfin.Sdk.Generated.Albums.Item.Similar.SimilarRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Albums.Item.WithItemItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithItemItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Albums/{itemId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Albums.Item.WithItemItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithItemItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Albums/{itemId}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618