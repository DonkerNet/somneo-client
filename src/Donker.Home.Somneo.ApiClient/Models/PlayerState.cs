using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the state of the Somneo's audio player.
    /// </summary>
    public sealed class PlayerState
    {
        /// <summary>
        /// Whether the audio player is enabled or not.
        /// </summary>
        [JsonProperty("onoff")]
        public bool Enabled { get; init; }
        /// <summary>
        /// The volume level currently set. Can be between 1 and 25.
        /// </summary>
        [JsonProperty("sdvol")]
        public int Volume { get; init; }
        /// <summary>
        /// The type of audio device in use by the Somneo's audio player.
        /// </summary>
        [JsonProperty("snddv")]
        public PlayerDeviceType Device { get; init; }
        /// <summary>
        /// The current channel or preset that is selected.
        /// </summary>
        [JsonProperty("sndch")]
        public string Channel { get; init; }
    }
}
