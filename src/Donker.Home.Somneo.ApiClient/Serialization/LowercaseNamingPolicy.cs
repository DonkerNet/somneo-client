using System.Text.Json;

namespace Donker.Home.Somneo.ApiClient.Serialization;

internal class LowercaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.ToLower();
}
