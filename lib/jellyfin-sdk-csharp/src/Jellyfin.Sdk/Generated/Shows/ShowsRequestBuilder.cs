// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Shows.Item;
using Jellyfin.Sdk.Generated.Shows.NextUp;
using Jellyfin.Sdk.Generated.Shows.Upcoming;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated.Shows
{
    /// <summary>
    /// Builds and executes requests for operations under \Shows
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class ShowsRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The NextUp property</summary>
        public global::Jellyfin.Sdk.Generated.Shows.NextUp.NextUpRequestBuilder NextUp
        {
            get => new global::Jellyfin.Sdk.Generated.Shows.NextUp.NextUpRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Upcoming property</summary>
        public global::Jellyfin.Sdk.Generated.Shows.Upcoming.UpcomingRequestBuilder Upcoming
        {
            get => new global::Jellyfin.Sdk.Generated.Shows.Upcoming.UpcomingRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>Gets an item from the Jellyfin.Sdk.Generated.Shows.item collection</summary>
        /// <param name="position">The item id.</param>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Shows.Item.ItemItemRequestBuilder"/></returns>
        public global::Jellyfin.Sdk.Generated.Shows.Item.ItemItemRequestBuilder this[Guid position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("item%2Did", position);
                return new global::Jellyfin.Sdk.Generated.Shows.Item.ItemItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Shows.ShowsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ShowsRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Shows", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Shows.ShowsRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public ShowsRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Shows", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618