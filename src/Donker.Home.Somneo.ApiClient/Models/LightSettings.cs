using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the light settings for the Somneo device.
    /// </summary>
    public sealed class LightSettings
    {
        [JsonProperty("onoff")]
        internal bool OnOff;
        [JsonProperty("tempy")]
        internal bool TempY;
        [JsonProperty("ngtlt")]
        internal bool NgtLt;

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
        /// Whether the night light is enabled or not.
        /// </summary>
        public bool SunrisePreviewEnabled => TempY;
    }
}
