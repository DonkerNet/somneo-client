using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models
{
    internal class AlarmStates
    {
        [JsonPropertyName("prfen")]
        internal bool[] Enabled { get; init; }
        [JsonPropertyName("prfvs")]
        internal bool[] Set { get; init; }
        [JsonPropertyName("pwrsv")]
        internal byte[] PowerWake { get; init; }
    }
}
