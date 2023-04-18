using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class SensorDataDto
{
    [JsonPropertyName("mstmp")]
    public float CurrentTemperature { get; set; }

    [JsonPropertyName("avtmp")]
    public float AverageTemperature { get; set; }

    [JsonPropertyName("mslux")]
    public float CurrentLight { get; set; }

    [JsonPropertyName("avlux")]
    public float AverageLight { get; set; }

    [JsonPropertyName("mssnd")]
    public float CurrentSound { get; set; }

    [JsonPropertyName("avsnd")]
    public float AverageSound { get; set; }

    [JsonPropertyName("msrhu")]
    public float CurrentHumidity { get; set; }

    [JsonPropertyName("avrhu")]
    public float AverageHumidity { get; set; }

    /* Example JSON:
{
  "mslux": 0.5,
  "mstmp": 16.7,
  "msrhu": 55.5,
  "mssnd": 36,
  "avlux": 0.6,
  "avtmp": 16.6,
  "avrhu": 56,
  "avsnd": 28
}
     */
}
