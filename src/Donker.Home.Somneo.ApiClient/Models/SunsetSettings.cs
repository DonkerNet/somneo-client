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
    /// Can be between 1 and 25.
    /// </summary>
    public int Intensity { get; }
    /// <summary>
    /// The duration of the sunset in minutes.
    /// Can be between 1 and 60.
    /// </summary>
    public int Duration { get; }
    /// <summary>
    /// The type of sunset colors shown.
    /// </summary>
    public ColorScheme Colors { get; }
    /// <summary>
    /// The type of sound device used for the sunset sound.
    /// </summary>
    public SoundDeviceType? SoundDevice { get; }
    /// <summary>
    /// The preset that is selected if <see cref="SoundDevice"/> is set to <see cref="SoundDeviceType.FMRadio"/>.
    /// Can be between 1 and 5.
    /// </summary>
    public int? FMRadioPreset { get; }
    /// <summary>
    /// The sunset sound that is selected if <see cref="SoundDevice"/> is set to <see cref="SoundDeviceType.Sunset"/>.
    /// </summary>
    public SunsetSound? Sound { get; }
    /// <summary>
    /// The sunset sound's volume level.
    /// Can be between 1 and 25.
    /// </summary>
    public int? Volume { get; }

    internal SunsetSettings(
        bool enabled,
        int intensity,
        int duration,
        ColorScheme colors,
        SoundDeviceType? soundDevice,
        int? fMRadioPreset,
        SunsetSound? sunsetSound,
        int? volume)
    {
        Enabled = enabled;
        Intensity = intensity;
        Duration = duration;
        Colors = colors;
        SoundDevice = soundDevice;
        FMRadioPreset = fMRadioPreset;
        Sound = sunsetSound;
        Volume = volume;
    }
}
