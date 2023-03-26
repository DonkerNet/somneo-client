using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Serialization.Converters;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the settings of a specific alarm of the Somneo device.
/// </summary>
public sealed class AlarmSettings
{
    private int? _powerWakeHour;
    private int? _powerWakeMinute;

    [JsonPropertyName("prfvs")]
    public bool IsSet { get; init; }

    [JsonPropertyName("daynm")]
    public DayFlags RepeatDayFlags
    {
        init => RepeatDays = new ReadOnlyCollection<DayOfWeek>(EnumHelper.DayFlagsToDaysOfWeek(value).ToList());
    }

    [JsonPropertyName("ctype")]
    public int ColorSchemeNumber { get; init; }

    [JsonPropertyName("pwrsz")]
    public int PowerWakeSize { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// On which days of the week the alarm is repeated.
    /// </summary>
    public IReadOnlyList<DayOfWeek> RepeatDays { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// The position of the alarm in the alarm list. Can be between 1 and 16.
    /// </summary>
    [JsonPropertyName("prfnr")]
    public int Position { get; init; }
    /// <summary>
    /// Whether the alarm is enabled or disabled.
    /// </summary>
    [JsonPropertyName("prfen")]
    public bool Enabled { get; init; }
    /// <summary>
    /// The hour of the alarm.
    /// </summary>
    [JsonPropertyName("almhr")]
    public int Hour { get; init; }
    /// <summary>
    /// The minute of the alarm.
    /// </summary>
    [JsonPropertyName("almmn")]
    public int Minute { get; init; }
    /// <summary>
    /// Whether the PowerWake function is enabled or not for this alarm.
    /// </summary>
    public bool PowerWakeEnabled => PowerWakeSize == 255;
    /// <summary>
    /// The hour of the PowerWake, if enabled.
    /// </summary>
    [JsonPropertyName("pszhr")]
    public int? PowerWakeHour
    {
        get => PowerWakeEnabled ? _powerWakeHour : null;
        init => _powerWakeHour = value;
    }
    /// <summary>
    /// The minute of the PowerWake, if enabled.
    /// </summary>
    [JsonPropertyName("pszmn")]
    public int? PowerWakeMinute
    {
        get => PowerWakeEnabled ? _powerWakeMinute : null;
        init => _powerWakeMinute = value;
    }
    /// <summary>
    /// The duration of the sunrise.
    /// </summary>
    [JsonPropertyName("durat")]
    public int SunriseDuration { get; init; }
    /// <summary>
    /// The maximum light level of the sunrise.
    /// </summary>
    [JsonPropertyName("curve")]
    public int SunriseIntensity { get; init; }
    /// <summary>
    /// The type of sunrise colors shown.
    /// </summary>
    public ColorScheme? SunriseColors => SunriseIntensity > 0 ? (ColorScheme)ColorSchemeNumber : null;
    /// <summary>
    /// The type of sound device used for the alarm sound.
    /// </summary>
    [JsonPropertyName("snddv")]
    [JsonConverter(typeof(EnumJsonConverter<SoundDeviceType, string>))]
    public SoundDeviceType Device { get; init; }
    /// <summary>
    /// The channel or preset that is selected for the alarm sound.
    /// </summary>
    [JsonPropertyName("sndch")]
    public string? ChannelOrPreset { get; init; }
    /// <summary>
    /// The alarm's volume level. Can be between 1 and 25.
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

    /* Example JSON:
{
  "prfnr": 1,
  "prfen": false,
  "prfvs": true,
  "pname": "Alarm",
  "ayear": 0,
  "amnth": 0,
  "alday": 0,
  "daynm": 254,
  "almhr": 7,
  "almmn": 0,
  "curve": 20,
  "durat": 30,
  "ctype": 0,
  "snddv": "wus",
  "sndch": "2",
  "sndlv": 15,
  "sndss": 30000,
  "pwrsz": 255,
  "pszhr": 7,
  "pszmn": 15
}
     */
}
