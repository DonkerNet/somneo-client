using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Serialization;

internal class SomneoApiSerializer
{
    private readonly MediaTypeHeaderValue _mediaType;
    private readonly JsonSerializerOptions _options;

    public SomneoApiSerializer()
    {
        var namingPolicy = new LowercaseNamingPolicy();

        _options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            DictionaryKeyPolicy = namingPolicy,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = namingPolicy,
            WriteIndented = false
        };

        _options.Converters.Add(new JsonStringEnumConverter(namingPolicy));

        _mediaType = new MediaTypeHeaderValue("application/json");
    }

    public HttpContent CreateHttpContent(object data)
    {
        string json = JsonSerializer.Serialize(data, data.GetType(), _options);
        return new StringContent(json, Encoding.UTF8, _mediaType);
    }

    public T? ReadHttpContent<T>(HttpContent content)
    {
        using var contentStream = content.ReadAsStream();
        return JsonSerializer.Deserialize<T>(contentStream, _options);
    }
}
