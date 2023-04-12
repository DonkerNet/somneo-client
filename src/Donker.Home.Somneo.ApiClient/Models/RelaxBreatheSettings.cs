namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the settings of the RelaxBreathe function of the Somneo device.
/// </summary>
public sealed class RelaxBreatheSettings
{
    /// <summary>
    /// Whether RelaxBreathe is enabled or disabled.
    /// </summary>
    public bool Enabled { get; }
    /// <summary>
    /// The duration of RelaxBreathe in minutes.
    /// Can be between 5 and 15.
    /// </summary>
    public int Duration { get; }
    /// <summary>
    /// The intensity of the light, if that is used to guide breathing.
    /// Can be between 1 and 25.
    /// </summary>
    public int? LightIntensity { get; }
    /// <summary>
    /// The volume of the sound, if that is used to guide breathing.
    /// Can be between 1 and 25.
    /// </summary>
    public int? SoundVolume { get; }
    /// <summary>
    /// The number of breaths to take per minute.
    /// </summary>
    public int BreathsPerMinute { get; }
    /// <summary>
    /// Whether light is used to guide breathing.
    /// </summary>
    public bool IsLight { get; }
    /// <summary>
    /// Whether sound is used to guide breathing.
    /// </summary>
    public bool IsSound { get; }

    internal RelaxBreatheSettings(
        bool enabled,
        int duration,
        int? lightIntensity,
        int? soundVolume,
        int breathsPerMinute,
        bool isLight,
        bool isSound)
    {
        Enabled = enabled;
        Duration = duration;
        LightIntensity = lightIntensity;
        SoundVolume = soundVolume;
        BreathsPerMinute = breathsPerMinute;
        IsLight = isLight;
        IsSound = isSound;
    }
}
