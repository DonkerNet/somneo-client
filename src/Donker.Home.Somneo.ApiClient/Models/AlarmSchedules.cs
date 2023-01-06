using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models
{
    internal class AlarmSchedules
    {
        [JsonPropertyName("daynm")]
        internal DayFlags[] RepeatDayFlags { get; init; }
        [JsonPropertyName("almhr")]
        internal int[] Hours { get; init; }
        [JsonPropertyName("almmn")]
        internal int[] Minutes { get; init; }
    }
}
