using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class PlayerStateDto
{
    [JsonPropertyName("onoff")]
    public bool Enabled { get; set; }

    [JsonPropertyName("sdvol")]
    public int Volume { get; set; }

    [JsonPropertyName("sndch")]
    public string? ChannelOrPreset { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("snddv")]
    public string Device { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /* Example JSON:
{
  "onoff": true,
  "sdvol": 4,
  "sdvch": 0,
  "tempy": false,
  "sndss": 0,
  "snddv": "fmr",
  "sndch": "1"
}
     */
}
