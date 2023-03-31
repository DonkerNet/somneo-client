using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Serialization.Converters;

internal class FloatJsonConverter : JsonConverter<float>
{
    public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
            return reader.GetSingle();

        string? valueString = reader.GetString();

        if (string.IsNullOrEmpty(valueString))
            return default;

        float.TryParse(valueString, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out float result);

        return result;
    }

    public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
    {
        string valueString = value.ToString(NumberFormatInfo.InvariantInfo);
        writer.WriteStringValue(valueString);
    }
}
