using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the state of the Somneo's FM radio
    /// </summary>
    public sealed class FMRadioState
    {
        /// <summary>
        /// The current frequency of the FM radio.
        /// </summary>
        [JsonProperty("fmfrq")]
        public float Frequency { get; init; }
        /// <summary>
        /// The current preset the frequency is set for. Can be between 1 and 5.
        /// </summary>
        [JsonProperty("prstn")]
        public int Preset { get; init; }
    }
}
