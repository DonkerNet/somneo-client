namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the state of the Somneo's audio player.
/// </summary>
public sealed class PlayerState
{
    /// <summary>
    /// Whether the audio player is enabled or not.
    /// </summary>
    public bool Enabled { get; }
    /// <summary>
    /// The volume level currently set.
    /// </summary>
    public int? Volume { get; }
    /// <summary>
    /// The type of sounds device in use by the Somneo's audio player.
    /// </summary>
    public SoundDeviceType? Device { get; }
    /// <summary>
    /// The preset that is selected if <see cref="Device"/> is set to <see cref="SoundDeviceType.FMRadio"/>.
    /// </summary>
    public int? FMRadioPreset { get; }
    /// <summary>
    /// The wake-up sound that is selected if <see cref="Device"/> is set to <see cref="SoundDeviceType.WakeUpSound"/>.
    /// </summary>
    public WakeUpSound? WakeUpSound { get; }
    /// <summary>
    /// The sunset sound that is selected if <see cref="Device"/> is set to <see cref="SoundDeviceType.Sunset"/>.
    /// </summary>
    public SunsetSound? SunsetSound { get; }

    internal PlayerState(
        bool enabled,
        int? volume,
        SoundDeviceType? device,
        int? fMRadioPreset,
        WakeUpSound? wakeUpSound,
        SunsetSound? sunsetSound)
    {
        Enabled = enabled;
        Volume = volume;
        Device = device;
        FMRadioPreset = fMRadioPreset;
        WakeUpSound = wakeUpSound;
        SunsetSound = sunsetSound;
    }
}
