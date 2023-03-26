using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the display state for the Somneo device.
/// </summary>
public sealed class DisplayState
{
    /// <summary>
    /// Whether the display is permanently shown or automatically disables after a period of time.
    /// </summary>
    [JsonPropertyName("dspon")]
    public bool Permanent { get; init; }
    /// <summary>
    /// The brightness level of the display.
    /// </summary>
    [JsonPropertyName("brght")]
    public int Brightness { get; init; }

    /* Example JSON:
{
  "wusts": 1,
  "rpair": false,
  "prvmd": false,
  "sdemo": false,
  "pwrsz": false,
  "nrcur": 4,
  "snztm": 5,
  "wizrd": 99,
  "brght": 4,
  "dspon": false,
  "canup": false,
  "fmrna": false,
  "wutim": 65535,
  "dutim": 65535,
  "sntim": 65535,
  "updtm": 65280,
  "updln": 65310
}
     */
}
