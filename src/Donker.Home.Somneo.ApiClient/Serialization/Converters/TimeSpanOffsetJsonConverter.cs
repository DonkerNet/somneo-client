using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Serialization.Converters
{
    public class TimeSpanOffsetJsonConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, [AllowNull] TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
                return TimeSpan.Zero;

            string valueString = reader.Value?.ToString();
            
            if (string.IsNullOrEmpty(valueString))
                return TimeSpan.Zero;

            char prefix = valueString[0];

            bool isNegative = prefix == '-';

            if (!char.IsDigit(prefix))
                valueString = valueString.Substring(1);

            bool success = TimeSpan.TryParseExact(valueString, "hh\\:mm", DateTimeFormatInfo.InvariantInfo, TimeSpanStyles.None, out TimeSpan offset);

            if (success)
                return isNegative ? -offset : offset;

            return TimeSpan.Zero;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] TimeSpan value, JsonSerializer serializer)
        {
            char prefix = value < TimeSpan.Zero ? '-' : '+';
            writer.WriteValue($"{prefix}{value:hh\\:mm}");
        }
    }
}
