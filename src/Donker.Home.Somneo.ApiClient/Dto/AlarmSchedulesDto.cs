using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class AlarmSchedulesDto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("daynm")]
    public byte[] RepeatDayFlags { get; set; }

    [JsonPropertyName("almhr")]
    public int[] Hours { get; set; }

    [JsonPropertyName("almmn")]
    public int[] Minutes { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
