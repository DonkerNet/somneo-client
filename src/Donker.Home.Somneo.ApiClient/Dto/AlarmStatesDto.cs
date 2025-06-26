using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class AlarmStatesDto
{
    [JsonPropertyName("prfen")]
    public required bool[] Enabled { get; set; }

    [JsonPropertyName("prfvs")]
    public required bool[] Set { get; set; }

    [JsonPropertyName("pwrsv")]
    public required int[] PowerWake { get; set; }

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
