using Donker.Home.Somneo.ApiClient.Serialization.Converters;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the state of the Somneo's audio player.
/// </summary>
public sealed class PlayerState
{
    /// <summary>
    /// Whether the audio player is enabled or not.
    /// </summary>
    [JsonPropertyName("onoff")]
    public bool Enabled { get; init; }
    /// <summary>
    /// The volume level currently set. Can be between 1 and 25.
    /// </summary>
    [JsonPropertyName("sdvol")]
    public int Volume { get; init; }
    /// <summary>
    /// The type of sounds device in use by the Somneo's audio player.
    /// </summary>
    [JsonPropertyName("snddv")]
    [JsonConverter(typeof(EnumJsonConverter<SoundDeviceType, string>))]
    public SoundDeviceType Device { get; init; }
    /// <summary>
    /// The current channel or preset that is selected.
    /// </summary>
    [JsonPropertyName("sndch")]
    public string? ChannelOrPreset { get; init; }

    /// <summary>
    /// Gets the FM-radio preset if the <see cref="Device"/> property is set to <see cref="SoundDeviceType.FMRadio"/>.
    /// </summary>
    public int? GetFMRadioPreset()
    {
        if (Device == SoundDeviceType.FMRadio
            && !string.IsNullOrEmpty(ChannelOrPreset)
            && int.TryParse(ChannelOrPreset, out int fmRadioPreset))
            return fmRadioPreset;

        return null;
    }

    /// <summary>
    /// Gets the wake-up sound if the <see cref="Device"/> property is set to <see cref="SoundDeviceType.WakeUpSound"/>.
    /// </summary>
    public WakeUpSound? GetWakeUpSound()
    {
        if (Device == SoundDeviceType.WakeUpSound
            && !string.IsNullOrEmpty(ChannelOrPreset)
            && Enum.TryParse(ChannelOrPreset, out WakeUpSound wakeUpSound))
            return wakeUpSound;

        return null;
    }

    /// <summary>
    /// Gets the wake-up sound if the <see cref="Device"/> property is set to <see cref="SoundDeviceType.Sunset"/>.
    /// </summary>
    public SunsetSound? GetSunsetSound()
    {
        if (Device == SoundDeviceType.Sunset
            && !string.IsNullOrEmpty(ChannelOrPreset)
            && Enum.TryParse(ChannelOrPreset, out SunsetSound sunsetSound))
            return sunsetSound;

        return null;
    }

    /* Example JSON:
{
  "onoff": true,
  "sdvol": 4,
  "sdvch": 0,
  "tempy": false,
  "sndss": 0,
  "snddv": "fmr",
  "sndch": "1"
}
     */
}
