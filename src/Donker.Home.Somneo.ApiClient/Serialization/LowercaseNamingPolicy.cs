using System.Text.Json;

namespace Donker.Home.Somneo.ApiClient.Serialization
{
    public class LowercaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name?.ToLower();
    }
}
