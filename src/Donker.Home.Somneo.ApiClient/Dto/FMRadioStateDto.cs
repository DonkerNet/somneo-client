using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class FMRadioStateDto
{
    [JsonPropertyName("fmfrq")]
    public float Frequency { get; set; }

    [JsonPropertyName("prstn")]
    public int Preset { get; set; }

    /* Example JSON:
{
  "fmfrq": "96.80",
  "fmcmd": "set",
  "fmsts": true,
  "fmspc": 100,
  "fmbnd": 0,
  "prstn": 2,
  "chtot": 5
}
     */
}
