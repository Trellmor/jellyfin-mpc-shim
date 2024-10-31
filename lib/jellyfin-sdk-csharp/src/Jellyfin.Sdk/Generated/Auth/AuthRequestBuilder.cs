// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Auth.Keys;
using Jellyfin.Sdk.Generated.Auth.PasswordResetProviders;
using Jellyfin.Sdk.Generated.Auth.Providers;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
namespace Jellyfin.Sdk.Generated.Auth
{
    /// <summary>
    /// Builds and executes requests for operations under \Auth
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AuthRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The Keys property</summary>
        public global::Jellyfin.Sdk.Generated.Auth.Keys.KeysRequestBuilder Keys
        {
            get => new global::Jellyfin.Sdk.Generated.Auth.Keys.KeysRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The PasswordResetProviders property</summary>
        public global::Jellyfin.Sdk.Generated.Auth.PasswordResetProviders.PasswordResetProvidersRequestBuilder PasswordResetProviders
        {
            get => new global::Jellyfin.Sdk.Generated.Auth.PasswordResetProviders.PasswordResetProvidersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Providers property</summary>
        public global::Jellyfin.Sdk.Generated.Auth.Providers.ProvidersRequestBuilder Providers
        {
            get => new global::Jellyfin.Sdk.Generated.Auth.Providers.ProvidersRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Auth.AuthRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AuthRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Auth", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Auth.AuthRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AuthRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Auth", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618