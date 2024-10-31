// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Playlists.Item.Items.Item.Move;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated.Playlists.Item.Items.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \Playlists\{item-id}\Items\{itemId}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithItemItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The Move property</summary>
        public global::Jellyfin.Sdk.Generated.Playlists.Item.Items.Item.Move.MoveRequestBuilder Move
        {
            get => new global::Jellyfin.Sdk.Generated.Playlists.Item.Items.Item.Move.MoveRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Playlists.Item.Items.Item.WithItemItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithItemItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Playlists/{item%2Did}/Items/{itemId}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Playlists.Item.Items.Item.WithItemItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithItemItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Playlists/{item%2Did}/Items/{itemId}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618