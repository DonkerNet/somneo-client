using System.Text.Json.Serialization;
using Donker.Home.Somneo.ApiClient.Serialization.Converters;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the time set for the Somneo device.
/// </summary>
public sealed class Time
{
    /// <summary>
    /// The date and time set for the device.
    /// </summary>
    public DateTimeOffset DateTime { get; init; }
    /// <summary>
    /// The UTC offset of the timezone set for the device.
    /// </summary>
    [JsonPropertyName("timezone")]
    [JsonConverter(typeof(TimeSpanOffsetJsonConverter))]
    public TimeSpan TimezoneOffset { get; init; }
    /// <summary>
    /// The offset that is applied to the date and time when DST is in progress.
    /// </summary>
    [JsonPropertyName("dst")]
    [JsonConverter(typeof(TimeSpanOffsetJsonConverter))]
    public TimeSpan CurrentDSTOffset { get; init; }
    /// <summary>
    /// The date and time of the next moment a daylight saving time transition occurs.
    /// </summary>
    public DateTimeOffset DSTChangeOver { get; init; }
    /// <summary>
    /// Whether daylight saving time is currently in progress and if the offset is applied to the date and time.
    /// </summary>
    public bool IsDSTApplied => CurrentDSTOffset != TimeSpan.Zero;

    /*    
    Example JSON without DST:
{
  "datetime": "2023-03-24T17:49:49+01:00",
  "dst": "+00:00",
  "dstchangeover": "2023-03-26T02:00:00+01:00",
  "dstoffset": "+01:00",
  "timezone": "+01:00",
  "calday": 5
}

    Example JSON with DST:
{
  "datetime": "2023-03-26T14:22:11+02:00",
  "dst": "+01:00",
  "dstchangeover": "2023-10-29T03:00:00+02:00",
  "dstoffset": "-01:00",
  "timezone": "+01:00",
  "calday": 7
}
     */
}
