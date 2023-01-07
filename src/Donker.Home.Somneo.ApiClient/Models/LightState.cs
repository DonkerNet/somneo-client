using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the light state for the Somneo device.
/// </summary>
public sealed class LightState
{
    [JsonPropertyName("onoff")]
    internal bool OnOff { get; init; }
    [JsonPropertyName("tempy")]
    internal bool TempY { get; init; }
    [JsonPropertyName("ngtlt")]
    internal bool NgtLt { get; init; }

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
    /// Whether the sunrise preview is enabled or not.
    /// </summary>
    public bool SunrisePreviewEnabled => TempY;
}
