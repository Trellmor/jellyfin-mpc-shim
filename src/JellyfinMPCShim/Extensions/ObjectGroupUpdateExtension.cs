using System.Text.Json;
using System.Text.Json.Nodes;
using Jellyfin.Sdk;
using Jellyfin.Sdk.JsonConverters;

namespace JellyfinMPCShim.Extensions;

public static class ObjectGroupUpdateExtension
{
    private static JsonSerializerOptions s_serializerOptions = new JsonSerializerOptions
    {
        Converters = { new JsonGuidConverter(), new JsonNullableGuidConverter() }
    };

    public static T? GetData<T>(this ObjectGroupUpdate objectGroupUpdate)
    {
        if (objectGroupUpdate.Data != null && objectGroupUpdate.Data is JsonElement json)
        {
            return json.Deserialize<T>(s_serializerOptions);
        }

        return default(T);
    }
}
