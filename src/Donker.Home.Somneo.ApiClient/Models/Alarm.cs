namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the schedule of an alarm that is set for the Somneo device.
/// </summary>
public sealed class Alarm
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// On which days of the week the alarm is repeated.
    /// </summary>
    public IReadOnlyList<DayOfWeek> RepeatDays { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// The position of the alarm in the alarm list. Can be between 1 and 16.
    /// </summary>
    public int Position { get; init; }
    /// <summary>
    /// Whether the alarm is enabled or disabled.
    /// </summary>
    public bool Enabled { get; init; }
    /// <summary>
    /// The hour of the alarm.
    /// </summary>
    public int Hour { get; init; }
    /// <summary>
    /// The minute of the alarm.
    /// </summary>
    public int Minute { get; init; }
    /// <summary>
    /// Whether the PowerWake function is enabled or not for this alarm.
    /// </summary>
    public bool PowerWakeEnabled { get; init; }
    /// <summary>
    /// The hour of the PowerWake, if enabled.
    /// </summary>
    public int? PowerWakeHour { get; init; }
    /// <summary>
    /// The minute of the PowerWake, if enabled.
    /// </summary>
    public int? PowerWakeMinute { get; init; }
}
