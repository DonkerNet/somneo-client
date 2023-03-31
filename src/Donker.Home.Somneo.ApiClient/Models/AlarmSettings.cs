using System.Collections.ObjectModel;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the settings of a specific alarm of the Somneo device.
/// </summary>
public sealed class AlarmSettings
{
    /// <summary>
    /// On which days of the week the alarm is repeated.
    /// </summary>
    public IReadOnlyList<DayOfWeek> RepeatDays { get; }
    /// <summary>
    /// The position of the alarm in the alarm list. Can be between 1 and 16.
    /// </summary>
    public int Position { get; }
    /// <summary>
    /// Whether the alarm is enabled or disabled.
    /// </summary>
    public bool Enabled { get; }
    /// <summary>
    /// The hour of the alarm.
    /// </summary>
    public int Hour { get; }
    /// <summary>
    /// The minute of the alarm.
    /// </summary>
    public int Minute { get; }
    /// <summary>
    /// Whether the PowerWake function is enabled or not for this alarm.
    /// </summary>
    public bool PowerWakeEnabled { get; }
    /// <summary>
    /// The hour of the PowerWake, if enabled.
    /// </summary>
    public int? PowerWakeHour { get; }
    /// <summary>
    /// The minute of the PowerWake, if enabled.
    /// </summary>
    public int? PowerWakeMinute { get; }
    /// <summary>
    /// The duration of the sunrise.
    /// </summary>
    public int? SunriseDuration { get; }
    /// <summary>
    /// The maximum light level of the sunrise.
    /// </summary>
    public int? SunriseIntensity { get; }
    /// <summary>
    /// The type of sunrise colors shown.
    /// </summary>
    public ColorScheme? SunriseColors { get; }
    /// <summary>
    /// The type of sound device used for the alarm sound.
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
    /// The alarm's volume level.
    /// </summary>
    public int? Volume { get; }

    internal AlarmSettings(
        IEnumerable<DayOfWeek> repeatDays,
        int position,
        bool enabled,
        int hour,
        int minute,
        bool powerWakeEnabled,
        int? powerWakeHour,
        int? powerWakeMinute,
        ColorScheme? sunriseColors,
        int? sunriseDuration,
        int? sunriseIntensity,
        SoundDeviceType? device,
        int? fMRadioPreset,
        WakeUpSound? wakeUpSound,
        int? volume)
    {
        RepeatDays = new ReadOnlyCollection<DayOfWeek>(repeatDays.ToList());
        Position = position;
        Enabled = enabled;
        Hour = hour;
        Minute = minute;
        PowerWakeEnabled = powerWakeEnabled;
        PowerWakeHour = powerWakeHour;
        PowerWakeMinute = powerWakeMinute;
        SunriseColors = sunriseColors;
        SunriseDuration = sunriseDuration;
        SunriseIntensity = sunriseIntensity;
        Device = device;
        FMRadioPreset = fMRadioPreset;
        WakeUpSound = wakeUpSound;
        Volume = volume;
    }
}
