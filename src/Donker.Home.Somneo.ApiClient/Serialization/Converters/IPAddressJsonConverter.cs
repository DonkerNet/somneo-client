using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Serialization.Converters
{
    public class IPAddressJsonConverter : JsonConverter<IPAddress>
    {
        public override IPAddress ReadJson(JsonReader reader, Type objectType, [AllowNull] IPAddress existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
                return null;

            string valueString = reader.Value?.ToString();
            
            if (string.IsNullOrEmpty(valueString))
                return null;

            IPAddress.TryParse(valueString, out IPAddress ipAddress);
            return ipAddress;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] IPAddress value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteValue(value.ToString());
        }
    }
}
