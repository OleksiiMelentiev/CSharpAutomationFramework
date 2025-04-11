using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.Api.Helpers;

public static class JsonHelper
{
    private static readonly JsonSerializerOptions OptionsD = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private static readonly JsonSerializerOptions OptionsS = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };


    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, OptionsD)
               ?? throw new SerializationException($"Can't deserialize json to {nameof(T)}");
    }
    
    public static string Serialize(object obj)
    {
        return  JsonSerializer.Serialize(obj, OptionsS);
    }
}