using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class AlarmSettingsDto
{
    [JsonPropertyName("prfvs")]
    public bool IsSet { get; set; }

    [JsonPropertyName("daynm")]
    public byte RepeatDayFlags { get; set; }

    [JsonPropertyName("ctype")]
    public int ColorSchemeNumber { get; set; }

    [JsonPropertyName("pwrsz")]
    public int PowerWakeSize { get; set; }

    [JsonPropertyName("prfnr")]
    public int Position { get; set; }

    [JsonPropertyName("prfen")]
    public bool Enabled { get; set; }

    [JsonPropertyName("almhr")]
    public int Hour { get; set; }

    [JsonPropertyName("almmn")]
    public int Minute { get; set; }

    [JsonPropertyName("pszhr")]
    public int PowerWakeHour { get; set; }

    [JsonPropertyName("pszmn")]
    public int PowerWakeMinute { get; set; }

    [JsonPropertyName("durat")]
    public int SunriseDuration { get; set; }

    [JsonPropertyName("curve")]
    public int SunriseIntensity { get; set; }

    [JsonPropertyName("sndch")]
    public string? ChannelOrPreset { get; set; }

    [JsonPropertyName("sndlv")]
    public int Volume { get; set; }

    [JsonPropertyName("snddv")]
    public required string Device { get; set; }

    /* Example JSON:
{
  "prfnr": 1,
  "prfen": false,
  "prfvs": true,
  "pname": "Alarm",
  "ayear": 0,
  "amnth": 0,
  "alday": 0,
  "daynm": 254,
  "almhr": 7,
  "almmn": 0,
  "curve": 20,
  "durat": 30,
  "ctype": 0,
  "snddv": "wus",
  "sndch": "2",
  "sndlv": 15,
  "sndss": 30000,
  "pwrsz": 255,
  "pszhr": 7,
  "pszmn": 15
}
     */
}
