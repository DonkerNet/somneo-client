﻿namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the locale set for the Somneo device.
    /// </summary>
    public sealed class Locale
    {
        /// <summary>
        /// The country set for the device.
        /// </summary>
        public string Country { get; init; }
        /// <summary>
        /// The timezone set for the device.
        /// </summary>
        public string Timezone { get; init; }
    }
}
