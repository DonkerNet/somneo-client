using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// A collection of FM radio frequency presents.
    /// </summary>
    public sealed class FMRadioPresets
    {
        /// <summary>
        /// The frequency of the first preset.
        /// </summary>
        [JsonProperty("1")]
        public float Preset1 { get; init; }
        /// <summary>
        /// The frequency of the second preset.
        /// </summary>
        [JsonProperty("2")]
        public float Preset2 { get; init; }
        /// <summary>
        /// The frequency of the third preset.
        /// </summary>
        [JsonProperty("3")]
        public float Preset3 { get; init; }
        /// <summary>
        /// The frequency of the fourth preset.
        /// </summary>
        [JsonProperty("4")]
        public float Preset4 { get; init; }
        /// <summary>
        /// The frequency of the fifth preset.
        /// </summary>
        [JsonProperty("5")]
        public float Preset5 { get; init; }
    }
}
