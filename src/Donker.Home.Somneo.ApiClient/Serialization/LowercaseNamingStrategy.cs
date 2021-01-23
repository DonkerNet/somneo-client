using Newtonsoft.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Serialization
{
    public class LowercaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name) => name?.ToLower();
    }
}
