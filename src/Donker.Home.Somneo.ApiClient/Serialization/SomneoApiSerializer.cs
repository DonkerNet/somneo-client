using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Donker.Home.Somneo.ApiClient.Serialization
{
    // JSON serialization settings for the API, automatically applied to the request body
    internal class SomneoApiSerializer : ISomneoApiSerializer
    {
        private static readonly Lazy<SomneoApiSerializer> LazyInstance;

        private readonly JsonSerializerSettings _settings;
        private const string _contentType = "application/json";

        public static SomneoApiSerializer Instance => LazyInstance.Value;

        public string ContentType
        {
            get { return _contentType; }
            set { }
        }

        static SomneoApiSerializer()
        {
            LazyInstance = new Lazy<SomneoApiSerializer>(() => new SomneoApiSerializer());
        }

        private SomneoApiSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new LowercaseNamingStrategy()
                }
            };
        }

        public string Serialize(object obj)
        {
            if (obj == null)
                return string.Empty;

            string json = JsonConvert.SerializeObject(obj, Formatting.None, _settings);
            return json;
        }

        public T Deserialize<T>(string content)
        {
            T obj = JsonConvert.DeserializeObject<T>(content, _settings);
            return obj;
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return Deserialize<T>(response.Content);
        }
    }
}
