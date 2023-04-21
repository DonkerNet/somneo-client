namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Information about a bedtime session.
/// </summary>
public sealed class BedtimeInfo
{
    /// <summary>
    /// Whether a bedtime session is in progress or not.
    /// </summary>
    public bool Enabled { get; }
    /// <summary>
    /// The moment the bedtime session started.
    /// </summary>
    public DateTimeOffset Started { get; }
    /// <summary>
    /// The moment the bedtime session ended.
    /// </summary>
    public DateTimeOffset? Ended { get; }
    /// <summary>
    /// The duration of the bedtime session.
    /// </summary>
    public TimeSpan? Duration { get; }

    internal BedtimeInfo(
        bool enabled,
        DateTimeOffset started,
        DateTimeOffset? ended)
    {
        Enabled = enabled;
        Started = started;
        Ended = ended;

        if (ended.HasValue)
            Duration = ended.Value - started;
    }
}
