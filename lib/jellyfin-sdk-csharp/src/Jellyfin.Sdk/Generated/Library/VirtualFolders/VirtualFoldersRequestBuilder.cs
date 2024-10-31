// <auto-generated/>
#pragma warning disable CS0618
using Jellyfin.Sdk.Generated.Library.VirtualFolders.LibraryOptions;
using Jellyfin.Sdk.Generated.Library.VirtualFolders.Name;
using Jellyfin.Sdk.Generated.Library.VirtualFolders.Paths;
using Jellyfin.Sdk.Generated.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace Jellyfin.Sdk.Generated.Library.VirtualFolders
{
    /// <summary>
    /// Builds and executes requests for operations under \Library\VirtualFolders
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class VirtualFoldersRequestBuilder : BaseRequestBuilder
    {
        /// <summary>The LibraryOptions property</summary>
        public global::Jellyfin.Sdk.Generated.Library.VirtualFolders.LibraryOptions.LibraryOptionsRequestBuilder LibraryOptions
        {
            get => new global::Jellyfin.Sdk.Generated.Library.VirtualFolders.LibraryOptions.LibraryOptionsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Name property</summary>
        public global::Jellyfin.Sdk.Generated.Library.VirtualFolders.Name.NameRequestBuilder Name
        {
            get => new global::Jellyfin.Sdk.Generated.Library.VirtualFolders.Name.NameRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The Paths property</summary>
        public global::Jellyfin.Sdk.Generated.Library.VirtualFolders.Paths.PathsRequestBuilder Paths
        {
            get => new global::Jellyfin.Sdk.Generated.Library.VirtualFolders.Paths.PathsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public VirtualFoldersRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Library/VirtualFolders{?collectionType*,name*,paths*,refreshLibrary*}", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public VirtualFoldersRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/Library/VirtualFolders{?collectionType*,name*,paths*,refreshLibrary*}", rawUrl)
        {
        }
        /// <summary>
        /// Removes a virtual folder.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task DeleteAsync(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderDeleteQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task DeleteAsync(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderDeleteQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToDeleteRequestInformation(requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Gets all virtual folders.
        /// </summary>
        /// <returns>A List&lt;global::Jellyfin.Sdk.Generated.Models.VirtualFolderInfo&gt;</returns>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task<List<global::Jellyfin.Sdk.Generated.Models.VirtualFolderInfo>?> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task<List<global::Jellyfin.Sdk.Generated.Models.VirtualFolderInfo>> GetAsync(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            var requestInfo = ToGetRequestInformation(requestConfiguration);
            var collectionResult = await RequestAdapter.SendCollectionAsync<global::Jellyfin.Sdk.Generated.Models.VirtualFolderInfo>(requestInfo, global::Jellyfin.Sdk.Generated.Models.VirtualFolderInfo.CreateFromDiscriminatorValue, default, cancellationToken).ConfigureAwait(false);
            return collectionResult?.AsList();
        }
        /// <summary>
        /// Adds a virtual folder.
        /// </summary>
        /// <param name="body">Add virtual folder dto.</param>
        /// <param name="cancellationToken">Cancellation token to use when cancelling requests</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public async Task PostAsync(global::Jellyfin.Sdk.Generated.Models.AddVirtualFolderDto body, Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderPostQueryParameters>>? requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#nullable restore
#else
        public async Task PostAsync(global::Jellyfin.Sdk.Generated.Models.AddVirtualFolderDto body, Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderPostQueryParameters>> requestConfiguration = default, CancellationToken cancellationToken = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = ToPostRequestInformation(body, requestConfiguration);
            await RequestAdapter.SendNoContentAsync(requestInfo, default, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Removes a virtual folder.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderDeleteQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToDeleteRequestInformation(Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderDeleteQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.DELETE, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            return requestInfo;
        }
        /// <summary>
        /// Gets all virtual folders.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToGetRequestInformation(Action<RequestConfiguration<DefaultQueryParameters>> requestConfiguration = default)
        {
#endif
            var requestInfo = new RequestInformation(Method.GET, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.Headers.TryAdd("Accept", "application/json");
            return requestInfo;
        }
        /// <summary>
        /// Adds a virtual folder.
        /// </summary>
        /// <returns>A <see cref="RequestInformation"/></returns>
        /// <param name="body">Add virtual folder dto.</param>
        /// <param name="requestConfiguration">Configuration for the request such as headers, query parameters, and middleware options.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public RequestInformation ToPostRequestInformation(global::Jellyfin.Sdk.Generated.Models.AddVirtualFolderDto body, Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderPostQueryParameters>>? requestConfiguration = default)
        {
#nullable restore
#else
        public RequestInformation ToPostRequestInformation(global::Jellyfin.Sdk.Generated.Models.AddVirtualFolderDto body, Action<RequestConfiguration<global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder.VirtualFoldersRequestBuilderPostQueryParameters>> requestConfiguration = default)
        {
#endif
            _ = body ?? throw new ArgumentNullException(nameof(body));
            var requestInfo = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
            requestInfo.Configure(requestConfiguration);
            requestInfo.SetContentFromParsable(RequestAdapter, "application/json", body);
            return requestInfo;
        }
        /// <summary>
        /// Returns a request builder with the provided arbitrary URL. Using this method means any other path or query parameters are ignored.
        /// </summary>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder"/></returns>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        public global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder WithUrl(string rawUrl)
        {
            return new global::Jellyfin.Sdk.Generated.Library.VirtualFolders.VirtualFoldersRequestBuilder(rawUrl, RequestAdapter);
        }
        /// <summary>
        /// Removes a virtual folder.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class VirtualFoldersRequestBuilderDeleteQueryParameters 
        {
            /// <summary>The name of the folder.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("name")]
            public string? Name { get; set; }
#nullable restore
#else
            [QueryParameter("name")]
            public string Name { get; set; }
#endif
            /// <summary>Whether to refresh the library.</summary>
            [QueryParameter("refreshLibrary")]
            public bool? RefreshLibrary { get; set; }
        }
        /// <summary>
        /// Adds a virtual folder.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
        public partial class VirtualFoldersRequestBuilderPostQueryParameters 
        {
            /// <summary>The type of the collection.</summary>
            [QueryParameter("collectionType")]
            public global::Jellyfin.Sdk.Generated.Library.VirtualFolders.CollectionTypeOptions? CollectionType { get; set; }
            /// <summary>The name of the virtual folder.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("name")]
            public string? Name { get; set; }
#nullable restore
#else
            [QueryParameter("name")]
            public string Name { get; set; }
#endif
            /// <summary>The paths of the virtual folder.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
            [QueryParameter("paths")]
            public string[]? Paths { get; set; }
#nullable restore
#else
            [QueryParameter("paths")]
            public string[] Paths { get; set; }
#endif
            /// <summary>Whether to refresh the library.</summary>
            [QueryParameter("refreshLibrary")]
            public bool? RefreshLibrary { get; set; }
        }
    }
}
#pragma warning restore CS0618