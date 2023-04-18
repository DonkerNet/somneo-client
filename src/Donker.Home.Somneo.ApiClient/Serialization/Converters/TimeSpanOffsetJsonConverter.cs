using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Serialization.Converters;

internal class TimeSpanOffsetJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            return TimeSpan.Zero;

        string? valueString = reader.GetString();

        if (string.IsNullOrEmpty(valueString))
            return TimeSpan.Zero;

        char prefix = valueString[0];

        bool isNegative = prefix == '-';

        if (!char.IsDigit(prefix))
            valueString = valueString[1..];

        bool success = TimeSpan.TryParseExact(valueString, "hh\\:mm", DateTimeFormatInfo.InvariantInfo, TimeSpanStyles.None, out TimeSpan offset);

        if (success)
            offset = isNegative ? -offset : offset;

        return offset;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        char prefix = value < TimeSpan.Zero ? '-' : '+';
        writer.WriteStringValue($"{prefix}{value:hh\\:mm}");
    }
}
