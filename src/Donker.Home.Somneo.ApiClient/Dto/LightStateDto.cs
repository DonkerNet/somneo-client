using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class LightStateDto
{
    [JsonPropertyName("onoff")]
    public bool Enabled { get; set; }

    [JsonPropertyName("tempy")]
    public bool SunriseOrSunsetEnabled { get; set; }

    [JsonPropertyName("ngtlt")]
    public bool NightLightEnabled { get; set; }

    [JsonPropertyName("ltlvl")]
    public int LightLevel { get; set; }

    /* Example JSON:
{
  "ltlvl": 15,
  "ltlch": 0,
  "onoff": false,
  "ctype": 0,
  "tempy": false,
  "ngtlt": false,
  "wucrv": [],
  "ltset": [],
  "pwmon": false,
  "pwmvs": [],
  "diman": 0
}
     */
}
