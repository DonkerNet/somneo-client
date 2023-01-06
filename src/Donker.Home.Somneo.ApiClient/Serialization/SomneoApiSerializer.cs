using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Serialization
{
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

        public HttpContent CreateHttpContent(object data) => JsonContent.Create(data, data.GetType(), _mediaType, _options);

        public T ReadHttpContent<T>(HttpContent content)
        {
            using var contentStream = content.ReadAsStream();
            return JsonSerializer.Deserialize<T>(contentStream, _options);
        }
    }
}
