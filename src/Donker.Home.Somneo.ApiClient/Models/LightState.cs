using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the light state for the Somneo device.
/// </summary>
public sealed class LightState
{
    [JsonPropertyName("onoff")]
    public bool OnOff { get; init; }
    [JsonPropertyName("tempy")]
    public bool TempY { get; init; }
    [JsonPropertyName("ngtlt")]
    public bool NgtLt { get; init; }

    /// <summary>
    /// Whether the light is enabled or not.
    /// </summary>
    public bool LightEnabled => OnOff && !TempY;
    /// <summary>
    /// The level of the normal light.
    /// </summary>
    [JsonPropertyName("ltlvl")]
    public int LightLevel { get; init; }
    /// <summary>
    /// Whether the night light is enabled or not.
    /// </summary>
    public bool NightLightEnabled => NgtLt;
    /// <summary>
    /// Whether the sunrise or sunset is enabled or not.
    /// </summary>
    public bool SunriseOrSunsetEnabled => TempY;

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
