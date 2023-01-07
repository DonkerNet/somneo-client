using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

internal class AlarmSchedules
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("daynm")]
    internal DayFlags[] RepeatDayFlags { get; init; }
    [JsonPropertyName("almhr")]
    internal int[] Hours { get; init; }
    [JsonPropertyName("almmn")]
    internal int[] Minutes { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
