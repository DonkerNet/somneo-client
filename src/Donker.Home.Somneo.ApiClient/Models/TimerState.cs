﻿using Newtonsoft.Json;
using System;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the current timer state for the Somneo device, used for the RelaxBreathe and Sunset functions.
    /// </summary>
    public sealed class TimerState
    {
        [JsonProperty("rlxmn")]
        internal int RelaxBreatheMinutes { get; init; }
        [JsonProperty("rlxsc")]
        internal int RelaxBreatheSeconds { get; init; }
        [JsonProperty("dskmn")]
        internal int SunsetMinutes { get; init; }
        [JsonProperty("dsksc")]
        internal int SunsetSeconds { get; init; }

        /// <summary>
        /// The initial start time of the timer.
        /// </summary>
        [JsonProperty("stime")]
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
        public TimeSpan? RelaxBreatheTime => RelaxBreatheEnabled ? TimeSpan.FromSeconds(RelaxBreatheMinutes * 60 + RelaxBreatheSeconds) : null;
        /// <summary>
        /// The time that was set for this timer, when enabled for the Sunset function.
        /// </summary>
        public TimeSpan? SunsetTime => SunsetEnabled ? TimeSpan.FromSeconds(SunsetMinutes * 60 + SunsetSeconds) : null;
    }
}
