namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Information about a bedtime session.
/// </summary>
public sealed class BedtimeInfo
{
    /// <summary>
    /// The moment the bedtime session started.
    /// </summary>
    public DateTimeOffset Started { get; }
    /// <summary>
    /// The moment the bedtime session ended.
    /// </summary>
    public DateTimeOffset Ended { get; }
    /// <summary>
    /// The duration of the bedtime session.
    /// </summary>
    public TimeSpan Duration { get; }

    internal BedtimeInfo(
        DateTimeOffset started,
        DateTimeOffset ended)
    {
        Started = started;
        Ended = ended;
        Duration = ended - started;
    }
}
