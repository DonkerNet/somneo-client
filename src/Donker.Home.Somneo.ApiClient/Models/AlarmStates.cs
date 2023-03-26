using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

public class AlarmStates
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("prfen")]
    public bool[] Enabled { get; init; }
    [JsonPropertyName("prfvs")]
    public bool[] Set { get; init; }
    [JsonPropertyName("pwrsv")]
    public int[] PowerWake { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /* Example JSON:
{
  "prfen": [
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false
  ],
  "prfvs": [
    true,
    true,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false,
    false
  ],
  "pwrsv": [
    255,
    7,
    15,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45,
    255,
    8,
    45
  ],
  "utcof": 60
}
     */
}
