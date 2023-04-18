using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class DisplayStateDto
{
    [JsonPropertyName("dspon")]
    public bool Permanent { get; set; }

    [JsonPropertyName("brght")]
    public int Brightness { get; set; }

    /* Example JSON:
{
  "wusts": 1,
  "rpair": false,
  "prvmd": false,
  "sdemo": false,
  "pwrsz": false,
  "nrcur": 4,
  "snztm": 5,
  "wizrd": 99,
  "brght": 4,
  "dspon": false,
  "canup": false,
  "fmrna": false,
  "wutim": 65535,
  "dutim": 65535,
  "sntim": 65535,
  "updtm": 65280,
  "updln": 65310
}
     */
}
