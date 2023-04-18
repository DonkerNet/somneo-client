using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.DemoApp.JsonConverters;

public class IPAddressJsonConverter : JsonConverter<IPAddress>
{
    public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? ipAddressString = reader.GetString();

        return !string.IsNullOrEmpty(ipAddressString)
            ? IPAddress.Parse(ipAddressString)
            : null;
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
