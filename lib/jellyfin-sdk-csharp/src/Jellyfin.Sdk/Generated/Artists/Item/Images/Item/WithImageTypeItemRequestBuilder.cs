// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Artists.Item.Images.Item.Item;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated.Artists.Item.Images.Item
{
    /// <summary>
    /// Builds and executes requests for operations under \Artists\{item-id}\Images\{imageType}
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class WithImageTypeItemRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the Jellyfin.Sdk.Generated.Artists.item.Images.item.item collection</summary>
        /// <param name="position">Image index.</param>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Artists.Item.Images.Item.Item.WithImageIndexItemRequestBuilder"/></returns>
        public global::Jellyfin.Sdk.Generated.Artists.Item.Images.Item.Item.WithImageIndexItemRequestBuilder this[int position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("imageIndex", position);
                return new global::Jellyfin.Sdk.Generated.Artists.Item.Images.Item.Item.WithImageIndexItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Artists.Item.Images.Item.WithImageTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithImageTypeItemRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Artists/{item%2Did}/Images/{imageType}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Artists.Item.Images.Item.WithImageTypeItemRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public WithImageTypeItemRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Artists/{item%2Did}/Images/{imageType}", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618