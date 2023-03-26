﻿using Donker.Home.Somneo.ApiClient.Serialization.Converters;
using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the settings of the Sunset function of the Somneo device.
/// </summary>
public sealed class SunsetSettings
{
    /// <summary>
    /// Whether the sunset is enabled or disabled.
    /// </summary>
    [JsonPropertyName("onoff")]
    public bool Enabled { get; init; }
    /// <summary>
    /// The maximum light level of the sunset.
    /// </summary>
    [JsonPropertyName("curve")]
    public int SunsetIntensity { get; init; }
    /// <summary>
    /// The duration of the sunset in minutes.
    /// </summary>
    [JsonPropertyName("durat")]
    public int SunsetDuration { get; init; }
    /// <summary>
    /// The type of sunset colors shown.
    /// </summary>
    [JsonPropertyName("ctype")]
    [JsonConverter(typeof(EnumJsonConverter<ColorScheme, int>))]
    public ColorScheme SunsetColors { get; init; }
    /// <summary>
    /// The type of sound device used for the sunset sound.
    /// </summary>
    [JsonPropertyName("snddv")]
    [JsonConverter(typeof(EnumJsonConverter<SoundDeviceType, string>))]
    public SoundDeviceType Device { get; init; }
    /// <summary>
    /// The channel or preset that is selected for the sunset sound.
    /// </summary>
    [JsonPropertyName("sndch")]
    public string? ChannelOrPreset { get; init; }
    /// <summary>
    /// The sunset sound's volume level. Can be between 1 and 25.
    /// </summary>
    [JsonPropertyName("sndlv")]
    public int Volume { get; init; }

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
    /// Gets the sunset sound if the <see cref="Device"/> property is set to <see cref="SoundDeviceType.Sunset"/>.
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
  "durat": 30,
  "onoff": false,
  "curve": 20,
  "ctype": 0,
  "sndtp": 1,
  "snddv": "dus",
  "sndch": "1",
  "sndlv": 12,
  "sndss": 200
}
     */
}
