using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.ApiClient.Validation;

// NOTE: Make sure the limits of these ranges are correctly mentioned in the ISomneoApiClient XML documentation!

/// <summary>
/// Ranges in which parameter values are considered to be valid and the steps in which they can be increased or decreased.
/// </summary>
public static class SomneoParameterValidators
{
    /// <summary>
    /// The minimum and maximum brightness of the light.
    /// </summary>
    public static RangeParameterValidator<int> LightLevel => new("light level", 1, 25);
    /// <summary>
    /// The minimum and maximum brightness of the display.
    /// </summary>
    public static RangeParameterValidator<int> DisplayLevel => new("display brightness level", 1, 6);
    /// <summary>
    /// The minimum and maximum volume.
    /// </summary>
    public static RangeParameterValidator<int> Volume => new("volume", 1, 25);
    /// <summary>
    /// The minimum and maximum preset number of the FM radio.
    /// </summary>
    public static RangeParameterValidator<int> FMRadioPreset => new("FM radio preset", 1, 6);
    /// <summary>
    /// The minimum and maximum position of an alarm.
    /// </summary>
    public static RangeParameterValidator<int> AlarmPosition => new("alarm position", 1, 16);
    /// <summary>
    /// The minimum and maximum hour of an alarm.
    /// </summary>
    public static RangeParameterValidator<int> AlarmHour => new("alarm hour", 0, 23);
    /// <summary>
    /// The minimum and maximum minute of an alarm.
    /// </summary>
    public static RangeParameterValidator<int> AlarmMinute => new("alarm minute", 0, 59);
    /// <summary>
    /// The minimum and maximum number of minutes for the PowerWake of an alarm.
    /// </summary>
    public static RangeParameterValidator<int> PowerWakeMinutes => new("PowerWake minutes", 0, 59);
    /// <summary>
    /// The minimum and maximum number of minutes to snooze for an alarm.
    /// </summary>
    public static RangeParameterValidator<int> SnoozeMinutes => new("snooze minutes", 0, 20);
    /// <summary>
    /// The minimum and maximum brightness of the sunrise.
    /// </summary>
    public static RangeParameterValidator<int> SunriseIntensity => new("sunrise intensity", 1, 25);
    /// <summary>
    /// The minimum and maximum duration of the sunrise and the number of steps in which it can be increased or decreased.
    /// </summary>
    public static RangeParameterValidator<int> SunriseDuration => new("sunrise duration", 5, 40, 5);
    /// <summary>
    /// The minimum and maximum brightness of the sunset.
    /// </summary>
    public static RangeParameterValidator<int> SunsetIntensity => new("sunset intensity", 1, 25);
    /// <summary>
    /// The minimum and maximum duration of the sunset and the number of steps in which it can be increased or decreased.
    /// </summary>
    public static RangeParameterValidator<int> SunsetDuration => new("sunset duration", 5, 60, 5);
    /// <summary>
    /// The minimum and maximum duration of RelaxBreathe and the number of steps in which it can be increased or decreased.
    /// </summary>
    public static RangeParameterValidator<int> RelaxBreatheDuration => new("RelaxBreathe duration", 5, 15, 5);
    /// <summary>
    /// The minimum and maximum number of breaths per minute for RelaxBreathe.
    /// </summary>
    public static RangeParameterValidator<int> RelaxBreatheBreathsPerMinute => new("RelaxBreathe breaths per minute", 4, 10);
    /// <summary>
    /// The minimum and maximum brightness for RelaxBreathe.
    /// </summary>
    public static RangeParameterValidator<int> RelaxBreatheLightIntensity => new("RelaxBreathe light intensity", 1, 25);
    /// <summary>
    /// The available wake-up sounds.
    /// </summary>
    public static EnumParameterValidator<WakeUpSound> WakeUpSound => new("wake-up sound");
    /// <summary>
    /// The available FM radio seek directions.
    /// </summary>
    public static EnumParameterValidator<RadioSeekDirection> RadioSeekDirection => new("FM radio seek direction");
    /// <summary>
    /// The available alarm repeat days.
    /// </summary>
    public static EnumParameterValidator<DayOfWeek> AlarmRepeatDay => new("alarm repeat day");
    /// <summary>
    /// The available sunrise colors.
    /// </summary>
    public static EnumParameterValidator<ColorScheme> SunriseColors => new("sunrise color scheme");
    /// <summary>
    /// The available sunset colors.
    /// </summary>
    public static EnumParameterValidator<ColorScheme> SunsetColors => new("sunset color scheme");
    /// <summary>
    /// The available sunset sounds.
    /// </summary>
    public static EnumParameterValidator<SunsetSound> SunsetSound => new("sunset sound");
}
