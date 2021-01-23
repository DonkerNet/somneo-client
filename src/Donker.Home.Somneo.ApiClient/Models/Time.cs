using System;
using Donker.Home.Somneo.ApiClient.Serialization.Converters;
using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the time set for the Somneo device.
    /// </summary>
    public sealed class Time
    {
        [JsonProperty("dst")]
        [JsonConverter(typeof(TimeSpanOffsetJsonConverter))]
        internal TimeSpan CurrentDSTOffset;

        /// <summary>
        /// The date and time set for the device.
        /// </summary>
        public DateTimeOffset DateTime { get; init; }
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
        /// The UTC offset of the timezone set for the device.
        /// </summary>
        [JsonProperty("timezone")]
        [JsonConverter(typeof(TimeSpanOffsetJsonConverter))]
        public TimeSpan TimezoneOffset { get; init; }
        /// <summary>
        /// Whether daylight saving time is currently in progress and if the offset is applied to the date and time.
        /// </summary>
        public bool IsDSTApplied => CurrentDSTOffset == DSTOffset;
    }
}
