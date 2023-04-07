using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class BedtimeInfoDto
{
    [JsonPropertyName("tg2bd")]
    public DateTimeOffset? Started { get; set; }

    [JsonPropertyName("tendb")]
    public DateTimeOffset? Ended { get; set; }

    /* Example JSON:
{
  "ntstr": "",
  "ntend": "07:00",
  "ntlen": "07:00",
  "night": false,
  "tg2bd": "2023-04-07T15:17:06+02:00",
  "tendb": "2023-04-07T15:17:19+02:00",
  "gdngt": false,
  "gdday": false
}
     */
}
