namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the settings of the Sunset function of the Somneo device.
/// </summary>
public sealed class SunsetSettings
{
    /// <summary>
    /// Whether the sunset is enabled or disabled.
    /// </summary>
    public bool Enabled { get; }
    /// <summary>
    /// The maximum light level of the sunset.
    /// </summary>
    public int SunsetIntensity { get; }
    /// <summary>
    /// The duration of the sunset in minutes.
    /// </summary>
    public int SunsetDuration { get; }
    /// <summary>
    /// The type of sunset colors shown.
    /// </summary>
    public ColorScheme SunsetColors { get; }
    /// <summary>
    /// The type of sound device used for the sunset sound.
    /// </summary>
    public SoundDeviceType? Device { get; }
    /// <summary>
    /// The preset that is selected if <see cref="Device"/> is set to <see cref="SoundDeviceType.FMRadio"/>.
    /// </summary>
    public int? FMRadioPreset { get; }
    /// <summary>
    /// The sunset sound that is selected if <see cref="Device"/> is set to <see cref="SoundDeviceType.Sunset"/>.
    /// </summary>
    public SunsetSound? SunsetSound { get; }
    /// <summary>
    /// The sunset sound's volume level.
    /// </summary>
    public int? Volume { get; }

    internal SunsetSettings(
        bool enabled,
        int sunsetIntensity,
        int sunsetDuration,
        ColorScheme sunsetColors,
        SoundDeviceType? device,
        int? fMRadioPreset,
        SunsetSound? sunsetSound,
        int? volume)
    {
        Enabled = enabled;
        SunsetIntensity = sunsetIntensity;
        SunsetDuration = sunsetDuration;
        SunsetColors = sunsetColors;
        Device = device;
        FMRadioPreset = fMRadioPreset;
        SunsetSound = sunsetSound;
        Volume = volume;
    }
}
