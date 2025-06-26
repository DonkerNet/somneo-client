using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class AlarmSchedulesDto
{
    [JsonPropertyName("daynm")]
    public required byte[] RepeatDayFlags { get; set; }

    [JsonPropertyName("almhr")]
    public required int[] Hours { get; set; }

    [JsonPropertyName("almmn")]
    public required int[] Minutes { get; set; }

    /*Example JSON:
{
  "ayear": [
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0
  ],
  "amnth": [
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0
  ],
  "alday": [
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0,
    0
  ],
  "daynm": [
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254,
    254
  ],
  "almhr": [
    7,
    8,
    7,
    7,
    7,
    7,
    7,
    7,
    7,
    7,
    7,
    7,
    7,
    7,
    7,
    7
  ],
  "almmn": [
    0,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30,
    30
  ]
}
     */
}
