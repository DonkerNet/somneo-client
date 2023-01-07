using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

internal class AlarmStates
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("prfen")]
    internal bool[] Enabled { get; init; }
    [JsonPropertyName("prfvs")]
    internal bool[] Set { get; init; }
    [JsonPropertyName("pwrsv")]
    internal byte[] PowerWake { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
