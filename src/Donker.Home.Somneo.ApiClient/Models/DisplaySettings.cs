using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the display settings for the Somneo device.
    /// </summary>
    public sealed class DisplaySettings
    {
        /// <summary>
        /// Whether the display is permanently shown or automatically disables after a period of time.
        /// </summary>
        [JsonProperty("dspon")]
        public bool Permanent { get; init; }
        /// <summary>
        /// The brightness level of the display.
        /// </summary>
        [JsonProperty("brght")]
        public int Brightness { get; init; }
    }
}
