using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    internal class AlarmSchedules
    {
        [JsonProperty("daynm")]
        public DayFlags[] RepeatDays { get; init; }
        [JsonProperty("almhr")]
        public int[] Hours { get; init; }
        [JsonProperty("almmn")]
        public int[] Minutes { get; init; }
    }
}
