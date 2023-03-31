namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes sensor data collection by the Somneo device.
/// </summary>
public sealed class SensorData
{
    /// <summary>
    /// The current temperature in °C.
    /// </summary>
    public float CurrentTemperature { get; }
    /// <summary>
    /// The average temperature in °C.
    /// </summary>
    public float AverageTemperature { get; }
    /// <summary>
    /// The current amount of light in lux.
    /// </summary>
    public float CurrentLight { get; }
    /// <summary>
    /// The average amount of light in lux.
    /// </summary>
    public float AverageLight { get; }
    /// <summary>
    /// The current sound level in dB.
    /// </summary>
    public float CurrentSound { get; }
    /// <summary>
    /// The average sound level in dB.
    /// </summary>
    public float AverageSound { get; }
    /// <summary>
    /// The current humidity in %.
    /// </summary>
    public float CurrentHumidity { get; }
    /// <summary>
    /// The average humidity in %.
    /// </summary>
    public float AverageHumidity { get; }

    internal SensorData(
        float currentTemperature,
        float averageTemperature,
        float currentLight,
        float averageLight,
        float currentSound,
        float averageSound,
        float currentHumidity,
        float averageHumidity)
    {
        CurrentTemperature = currentTemperature;
        AverageTemperature = averageTemperature;
        CurrentLight = currentLight;
        AverageLight = averageLight;
        CurrentSound = currentSound;
        AverageSound = averageSound;
        CurrentHumidity = currentHumidity;
        AverageHumidity = averageHumidity;
    }
}
