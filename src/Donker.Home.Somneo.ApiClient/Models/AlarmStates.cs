using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    internal class AlarmStates
    {
        [JsonProperty("prfen")]
        internal bool[] Enabled { get; init; }
        [JsonProperty("prfvs")]
        internal bool[] Set { get; init; }
        [JsonProperty("pwrsv")]
        internal byte[] PowerWake { get; init; }
    }
}
