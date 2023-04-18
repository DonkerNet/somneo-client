namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the time set for the Somneo device.
/// </summary>
public sealed class Time
{
    /// <summary>
    /// The date and time set for the device.
    /// </summary>
    public DateTimeOffset DateTime { get; }
    /// <summary>
    /// The UTC offset of the timezone set for the device.
    /// </summary>
    public TimeSpan TimezoneOffset { get; }
    /// <summary>
    /// The offset that is applied to the date and time when DST is in progress.
    /// </summary>
    public TimeSpan CurrentDSTOffset { get; }
    /// <summary>
    /// The date and time of the next moment a daylight saving time transition occurs.
    /// </summary>
    public DateTimeOffset DSTChangeOver { get; }
    /// <summary>
    /// Whether daylight saving time is currently in progress and if the offset is applied to the date and time.
    /// </summary>
    public bool IsDSTApplied { get; }

    internal Time(
        DateTimeOffset dateTime,
        TimeSpan timezoneOffset,
        TimeSpan currentDSTOffset,
        DateTimeOffset dstChangeOver)
    {
        DateTime = dateTime;
        TimezoneOffset = timezoneOffset;
        CurrentDSTOffset = currentDSTOffset;
        DSTChangeOver = dstChangeOver;
        IsDSTApplied = currentDSTOffset != TimeSpan.Zero;
    }
}
