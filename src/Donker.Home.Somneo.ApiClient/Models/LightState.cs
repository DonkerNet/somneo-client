namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the light state for the Somneo device.
/// </summary>
public sealed class LightState
{
    /// <summary>
    /// Whether the light is enabled or not.
    /// </summary>
    public bool LightEnabled { get; }
    /// <summary>
    /// The level of the normal light.
    /// </summary>
    public int LightLevel { get; }
    /// <summary>
    /// Whether the night light is enabled or not.
    /// </summary>
    public bool NightLightEnabled { get; }
    /// <summary>
    /// Whether the sunrise or sunset is enabled or not.
    /// </summary>
    public bool SunriseOrSunsetEnabled { get; }

    internal LightState(
        bool lightEnabled,
        int lightLevel,
        bool nightLightEnabled,
        bool sunriseOrSunsetEnabled)
    {
        LightEnabled = lightEnabled;
        LightLevel = lightLevel;
        NightLightEnabled = nightLightEnabled;
        SunriseOrSunsetEnabled = sunriseOrSunsetEnabled;
    }
}
