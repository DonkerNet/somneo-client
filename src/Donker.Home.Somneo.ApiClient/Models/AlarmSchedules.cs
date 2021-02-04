using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    internal class AlarmSchedules
    {
        [JsonProperty("daynm")]
        internal DayFlags[] RepeatDayFlags { get; init; }
        [JsonProperty("almhr")]
        internal int[] Hours { get; init; }
        [JsonProperty("almmn")]
        internal int[] Minutes { get; init; }
    }
}
