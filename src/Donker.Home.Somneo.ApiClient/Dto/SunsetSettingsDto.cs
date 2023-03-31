using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class SunsetSettingsDto
{
    [JsonPropertyName("onoff")]
    public bool Enabled { get; set; }

    [JsonPropertyName("curve")]
    public int SunsetIntensity { get; set; }

    [JsonPropertyName("durat")]
    public int SunsetDuration { get; set; }

    [JsonPropertyName("ctype")]
    public int SunsetColors { get; set; }

    [JsonPropertyName("sndch")]
    public string? ChannelOrPreset { get; set; }

    [JsonPropertyName("sndlv")]
    public int Volume { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("snddv")]
    public string Device { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
