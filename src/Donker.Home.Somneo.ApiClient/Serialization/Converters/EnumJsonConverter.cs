using Donker.Home.Somneo.ApiClient.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Serialization.Converters;

public class EnumJsonConverter<TEnum, TSerialized> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? valueString = null;

        if (reader.TokenType == JsonTokenType.Number)
            valueString = reader.GetInt32().ToString();
        else if (reader.TokenType == JsonTokenType.String)
            valueString = reader.GetString();

        if (string.IsNullOrEmpty(valueString))
            return default;

        var enumValues = Enum.GetValues<TEnum>();

        foreach (TEnum enumValue in enumValues)
        {
            string? enumMemberValue = EnumHelper.GetEnumMemberValue(enumValue);
            if (string.Equals(valueString, enumMemberValue, StringComparison.OrdinalIgnoreCase))
                return enumValue;
        }

        return Enum.TryParse(valueString, true, out TEnum result) ? result : default;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (typeof(TSerialized) == typeof(string))
        {
            string? valueString = EnumHelper.GetEnumMemberValue(value) ?? Enum.GetName(value);
            if (!string.IsNullOrEmpty(valueString))
            {
                writer.WriteStringValue(valueString);
                return;
            }
        }

        writer.WriteNumberValue(Convert.ToInt32(value));
    }
}
