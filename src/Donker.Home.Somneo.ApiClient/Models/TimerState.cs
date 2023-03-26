using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the current timer state for the Somneo device, used for the RelaxBreathe and sunset functions.
/// </summary>
public sealed class TimerState
{
    [JsonPropertyName("rlxmn")]
    public int RelaxBreatheMinutes { get; init; }
    [JsonPropertyName("rlxsc")]
    public int RelaxBreatheSeconds { get; init; }
    [JsonPropertyName("dskmn")]
    public int SunsetMinutes { get; init; }
    [JsonPropertyName("dsksc")]
    public int SunsetSeconds { get; init; }

    /// <summary>
    /// The initial start time of the timer.
    /// </summary>
    [JsonPropertyName("stime")]
    public DateTimeOffset? StartTime { get; init; }
    /// <summary>
    /// Whether the timer is enabled for the RelaxBreathe function or not.
    /// </summary>
    public bool RelaxBreatheEnabled => RelaxBreatheMinutes > 0 || RelaxBreatheSeconds > 0;
    /// <summary>
    /// Whether the timer is enabled for the Sunset function or not.
    /// </summary>
    public bool SunsetEnabled => SunsetMinutes > 0 || SunsetSeconds > 0;
    /// <summary>
    /// Whether the timer is enabled or not.
    /// </summary>
    public bool Enabled => RelaxBreatheEnabled || SunsetEnabled;
    /// <summary>
    /// The time that was set for this timer, when enabled for the RelaxBreathe function.
    /// </summary>
    public TimeSpan? RelaxBreatheTime => RelaxBreatheEnabled ? TimeSpan.FromSeconds((RelaxBreatheMinutes * 60) + RelaxBreatheSeconds) : null;
    /// <summary>
    /// The time that was set for this timer, when enabled for the Sunset function.
    /// </summary>
    public TimeSpan? SunsetTime => SunsetEnabled ? TimeSpan.FromSeconds((SunsetMinutes * 60) + SunsetSeconds) : null;

    /*
{
  "stime": "2023-03-24T19:28:24+01:00",
  "rlxmn": 0,
  "rlxsc": 0,
  "dskmn": 29,
  "dsksc": 59
}
     */
}
