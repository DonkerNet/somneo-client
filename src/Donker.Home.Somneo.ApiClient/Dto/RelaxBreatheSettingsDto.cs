using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class RelaxBreatheSettingsDto
{
    [JsonPropertyName("onoff")]
    public bool Enabled { get; set; }

    [JsonPropertyName("durat")]
    public int Duration { get; set; }

    [JsonPropertyName("intny")]
    public int Intensity { get; set; }

    [JsonPropertyName("sndlv")]
    public int Volume { get; set; }

    [JsonPropertyName("progr")]
    public int Program { get; set; }

    [JsonPropertyName("rtype")]
    public int Type { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("rlbpm")]
    public int[] AvailableBpms { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    /* Example JSON:
{
  "durat": 15,
  "onoff": true,
  "maxpr": 7,
  "progr": 2,
  "rtype": 1,
  "intny": 20,
  "sndlv": 17,
  "sndss": 200,
  "rlbpm": [
    4,
    5,
    6,
    7,
    8,
    9,
    10
  ],
  "pause": [
    20,
    20,
    20,
    20,
    10,
    10,
    10
  ]
}
     */
}
