namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the current timer state for the Somneo device, used for the RelaxBreathe and sunset functions.
/// </summary>
public sealed class TimerState
{
    /// <summary>
    /// The initial start time of the timer.
    /// </summary>
    public DateTimeOffset? StartTime { get; }
    /// <summary>
    /// Whether the timer is enabled for the RelaxBreathe function or not.
    /// </summary>
    public bool RelaxBreatheEnabled { get; }
    /// <summary>
    /// Whether the timer is enabled for the Sunset function or not.
    /// </summary>
    public bool SunsetEnabled { get; }
    /// <summary>
    /// Whether the timer is enabled or not.
    /// </summary>
    public bool Enabled { get; }
    /// <summary>
    /// The time that was set for this timer, when enabled for the RelaxBreathe function.
    /// </summary>
    public TimeSpan? RelaxBreatheTime { get; }
    /// <summary>
    /// The time that was set for this timer, when enabled for the Sunset function.
    /// </summary>
    public TimeSpan? SunsetTime { get; }

    internal TimerState(
        DateTimeOffset? startTime,
        bool relaxBreatheEnabled,
        bool sunsetEnabled,
        bool enabled,
        TimeSpan? relaxBreatheTime,
        TimeSpan? sunsetTime)
    {
        StartTime = startTime;
        RelaxBreatheEnabled = relaxBreatheEnabled;
        SunsetEnabled = sunsetEnabled;
        Enabled = enabled;
        RelaxBreatheTime = relaxBreatheTime;
        SunsetTime = sunsetTime;
    }
}
