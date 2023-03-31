using System.Collections.ObjectModel;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the schedule of an alarm that is set for the Somneo device.
/// </summary>
public sealed class Alarm
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

    internal Alarm(
        IEnumerable<DayOfWeek> repeatDays,
        int position,
        bool enabled,
        int hour,
        int minute,
        bool powerWakeEnabled,
        int? powerWakeHour,
        int? powerWakeMinute)
    {
        RepeatDays = new ReadOnlyCollection<DayOfWeek>(repeatDays.ToList());
        Position = position;
        Enabled = enabled;
        Hour = hour;
        Minute = minute;
        PowerWakeEnabled = powerWakeEnabled;
        PowerWakeHour = powerWakeHour;
        PowerWakeMinute = powerWakeMinute;
    }
}
