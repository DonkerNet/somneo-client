using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    internal class AlarmStates
    {
        [JsonProperty("prfen")]
        public bool[] Enabled { get; init; }
        [JsonProperty("prfvs")]
        public bool[] Set { get; init; }
        [JsonProperty("pwrsv")]
        public int[] PowerWake { get; init; }
    }
}
