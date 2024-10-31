// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.PlayingItems.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated.PlayingItems
{
    /// <summary>
    /// Builds and executes requests for operations under \PlayingItems
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class PlayingItemsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the Jellyfin.Sdk.Generated.PlayingItems.item collection</summary>
        /// <param name="position">Item id.</param>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.PlayingItems.Item.WithItemItemRequestBuilder"/></returns>
        public global::Jellyfin.Sdk.Generated.PlayingItems.Item.WithItemItemRequestBuilder this[Guid position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("itemId", position);
                return new global::Jellyfin.Sdk.Generated.PlayingItems.Item.WithItemItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.PlayingItems.PlayingItemsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public PlayingItemsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/PlayingItems", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.PlayingItems.PlayingItemsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public PlayingItemsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/PlayingItems", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618