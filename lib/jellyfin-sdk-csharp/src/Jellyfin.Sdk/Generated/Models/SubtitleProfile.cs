// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Jellyfin.Sdk.Generated.Models
{
    /// <summary>
    /// A class for subtitle profile information.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SubtitleProfile : IParsable
    {
        /// <summary>Gets or sets the container.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Container { get; set; }
#nullable restore
#else
        public string Container { get; set; }
#endif
        /// <summary>Gets or sets the DIDL mode.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? DidlMode { get; set; }
#nullable restore
#else
        public string DidlMode { get; set; }
#endif
        /// <summary>Gets or sets the format.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Format { get; set; }
#nullable restore
#else
        public string Format { get; set; }
#endif
        /// <summary>Gets or sets the language.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Language { get; set; }
#nullable restore
#else
        public string Language { get; set; }
#endif
        /// <summary>Gets or sets the delivery method.</summary>
        public global::Jellyfin.Sdk.Generated.Models.SubtitleProfile_Method? Method { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Jellyfin.Sdk.Generated.Models.SubtitleProfile"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Jellyfin.Sdk.Generated.Models.SubtitleProfile CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::Jellyfin.Sdk.Generated.Models.SubtitleProfile();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "Container", n => { Container = n.GetStringValue(); } },
                { "DidlMode", n => { DidlMode = n.GetStringValue(); } },
                { "Format", n => { Format = n.GetStringValue(); } },
                { "Language", n => { Language = n.GetStringValue(); } },
                { "Method", n => { Method = n.GetEnumValue<global::Jellyfin.Sdk.Generated.Models.SubtitleProfile_Method>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("Container", Container);
            writer.WriteStringValue("DidlMode", DidlMode);
            writer.WriteStringValue("Format", Format);
            writer.WriteStringValue("Language", Language);
            writer.WriteEnumValue<global::Jellyfin.Sdk.Generated.Models.SubtitleProfile_Method>("Method", Method);
        }
    }
}
#pragma warning restore CS0618