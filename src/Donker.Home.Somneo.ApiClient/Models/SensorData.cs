using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes sensor data collection by the Somneo device.
/// </summary>
public sealed class SensorData
{
    /// <summary>
    /// The current temperature in °C.
    /// </summary>
    [JsonPropertyName("mstmp")]
    public float CurrentTemperature { get; init; }
    /// <summary>
    /// The average temperature in °C.
    /// </summary>
    [JsonPropertyName("avtmp")]
    public float AverageTemperature { get; init; }
    /// <summary>
    /// The current amount of light in lux.
    /// </summary>
    [JsonPropertyName("mslux")]
    public float CurrentLight { get; init; }
    /// <summary>
    /// The average amount of light in lux.
    /// </summary>
    [JsonPropertyName("avlux")]
    public float AverageLight { get; init; }
    /// <summary>
    /// The current sound level in dB.
    /// </summary>
    [JsonPropertyName("mssnd")]
    public float CurrentSound { get; init; }
    /// <summary>
    /// The average sound level in dB.
    /// </summary>
    [JsonPropertyName("avsnd")]
    public float AverageSound { get; init; }
    /// <summary>
    /// The current humidity in %.
    /// </summary>
    [JsonPropertyName("msrhu")]
    public float CurrentHumidity { get; init; }
    /// <summary>
    /// The average humidity in %.
    /// </summary>
    [JsonPropertyName("avhum")]
    public float AverageHumidity { get; init; }
}
