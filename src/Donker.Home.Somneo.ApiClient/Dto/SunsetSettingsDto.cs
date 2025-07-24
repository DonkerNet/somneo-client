using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class SunsetSettingsDto
{
    [JsonPropertyName("onoff")]
    public bool Enabled { get; set; }

    [JsonPropertyName("curve")]
    public int Intensity { get; set; }

    [JsonPropertyName("durat")]
    public int Duration { get; set; }

    [JsonPropertyName("ctype")]
    public int Colors { get; set; }

    [JsonPropertyName("sndch")]
    public string? ChannelOrPreset { get; set; }

    [JsonPropertyName("sndlv")]
    public int Volume { get; set; }

    [JsonPropertyName("snddv")]
    public required string SoundDevice { get; set; }

    /* Example JSON:
{
  "durat": 30,
  "onoff": false,
  "curve": 20,
  "ctype": 0,
  "sndtp": 1,
  "snddv": "dus",
  "sndch": "1",
  "sndlv": 12,
  "sndss": 200
}
     */
}
