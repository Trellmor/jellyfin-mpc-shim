﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jellyfin.Sdk.JsonConverters;

/// <summary>
/// Converts a GUID object or value to/from JSON.
/// </summary>
public class JsonGuidConverter : JsonConverter<Guid>
{
    /// <inheritdoc />
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guidStr = reader.GetString();
        return guidStr == null ? Guid.Empty : new Guid(guidStr);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
