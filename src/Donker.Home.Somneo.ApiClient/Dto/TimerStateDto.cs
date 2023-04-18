using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class TimerStateDto
{
    [JsonPropertyName("rlxmn")]
    public int RelaxBreatheMinutes { get; set; }

    [JsonPropertyName("rlxsc")]
    public int RelaxBreatheSeconds { get; set; }

    [JsonPropertyName("dskmn")]
    public int SunsetMinutes { get; set; }

    [JsonPropertyName("dsksc")]
    public int SunsetSeconds { get; set; }

    [JsonPropertyName("stime")]
    public DateTimeOffset? StartTime { get; set; }

    /*
{
  "stime": "2023-03-24T19:28:24+01:00",
  "rlxmn": 0,
  "rlxsc": 0,
  "dskmn": 29,
  "dsksc": 59
}
     */
}
