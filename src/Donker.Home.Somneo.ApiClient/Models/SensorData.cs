using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes sensor data collection by the Somneo device.
    /// </summary>
    public sealed class SensorData
    {
        /// <summary>
        /// The current temperature in °C.
        /// </summary>
        [JsonProperty("mstmp")]
        public float CurrentTemperature { get; init; }
        /// <summary>
        /// The average temperature in °C.
        /// </summary>
        [JsonProperty("avtmp")]
        public float AverageTemperature { get; init; }
        /// <summary>
        /// The current amount of light in lux.
        /// </summary>
        [JsonProperty("mslux")]
        public float CurrentLight { get; init; }
        /// <summary>
        /// The average amount of light in lux.
        /// </summary>
        [JsonProperty("avlux")]
        public float AverageLight { get; init; }
        /// <summary>
        /// The current sound level in dB.
        /// </summary>
        [JsonProperty("mssnd")]
        public float CurrentSound { get; init; }
        /// <summary>
        /// The average sound level in dB.
        /// </summary>
        [JsonProperty("avsnd")]
        public float AverageSound { get; init; }
        /// <summary>
        /// The current humidity in %.
        /// </summary>
        [JsonProperty("msrhu")]
        public float CurrentHumidity { get; init; }
        /// <summary>
        /// The average humidity in %.
        /// </summary>
        [JsonProperty("avhum")]
        public float AverageHumidity { get; init; }
    }
}
