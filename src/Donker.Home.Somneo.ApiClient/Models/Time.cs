using System;
using System.Text.Json.Serialization;
using Donker.Home.Somneo.ApiClient.Serialization.Converters;

namespace Donker.Home.Somneo.ApiClient.Models
{
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
        /// The offset that is applied to the date and time when daylight saving time is in progress.
        /// </summary>
        [JsonConverter(typeof(TimeSpanOffsetJsonConverter))]
        public TimeSpan DSTOffset { get; init; }
        /// <summary>
        /// Whether daylight saving time is currently in progress and if the offset is applied to the date and time.
        /// </summary>
        public bool IsDSTApplied => CurrentDSTOffset == DSTOffset;

        /*
         
        TODO:

        Check the above DST properties after next DST switch, to see if it's properly implemented.

        What is the difference between "dst" and "dstoffset" in the JSON returned by Somneo?
        "dst" = current offset, "dstoffset" = next offset ???
        How can we use these to check if DST is currently applied?
        What if the locale doesn't use DST?

        Example with DST applied:
        { "datetime":"2021-07-22T18:27:37+02:00", "dst":"+01:00", "dstchangeover":"2021-10-31T03:00:00+02:00", "dstoffset":"-01:00", "timezone":"+01:00", "calday":4 }

        Example without:
        ???
        
         */
    }
}
