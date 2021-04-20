using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the light state for the Somneo device.
    /// </summary>
    public sealed class LightState
    {
        [JsonProperty("onoff")]
        internal bool OnOff { get; init; }
        [JsonProperty("tempy")]
        internal bool TempY { get; init; }
        [JsonProperty("ngtlt")]
        internal bool NgtLt { get; init; }

        /// <summary>
        /// Whether the light is enabled or not.
        /// </summary>
        public bool LightEnabled => OnOff && !TempY;
        /// <summary>
        /// The level of the normal light.
        /// </summary>
        [JsonProperty("ltlvl")]
        public int LightLevel { get; init; }
        /// <summary>
        /// Whether the night light is enabled or not.
        /// </summary>
        public bool NightLightEnabled => NgtLt;
        /// <summary>
        /// Whether the sunrise preview is enabled or not.
        /// </summary>
        public bool SunrisePreviewEnabled => TempY;
    }
}
