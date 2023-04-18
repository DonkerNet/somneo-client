using System.Text.Json.Serialization;
using Donker.Home.Somneo.ApiClient.Serialization.Converters;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class TimeDto
{
    public DateTimeOffset DateTime { get; set; }

    [JsonPropertyName("timezone")]
    [JsonConverter(typeof(TimeSpanOffsetJsonConverter))]
    public TimeSpan TimezoneOffset { get; set; }

    [JsonPropertyName("dst")]
    [JsonConverter(typeof(TimeSpanOffsetJsonConverter))]
    public TimeSpan CurrentDSTOffset { get; set; }

    public DateTimeOffset DSTChangeOver { get; set; }

    /*    
    Example JSON without DST:
{
  "datetime": "2023-03-24T17:49:49+01:00",
  "dst": "+00:00",
  "dstchangeover": "2023-03-26T02:00:00+01:00",
  "dstoffset": "+01:00",
  "timezone": "+01:00",
  "calday": 5
}

    Example JSON with DST:
{
  "datetime": "2023-03-26T14:22:11+02:00",
  "dst": "+01:00",
  "dstchangeover": "2023-10-29T03:00:00+02:00",
  "dstoffset": "-01:00",
  "timezone": "+01:00",
  "calday": 7
}
     */
}
