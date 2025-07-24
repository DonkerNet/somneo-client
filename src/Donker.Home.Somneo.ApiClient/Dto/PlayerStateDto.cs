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

    [JsonPropertyName("snddv")]
    public required string SoundDevice { get; set; }

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
