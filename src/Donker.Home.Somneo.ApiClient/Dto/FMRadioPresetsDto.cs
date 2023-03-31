using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class FMRadioPresetsDto
{
    [JsonPropertyName("1")]
    public float Preset1 { get; set; }

    [JsonPropertyName("2")]
    public float Preset2 { get; set; }

    [JsonPropertyName("3")]
    public float Preset3 { get; set; }

    [JsonPropertyName("4")]
    public float Preset4 { get; set; }

    [JsonPropertyName("5")]
    public float Preset5 { get; set; }

    /* Example JSON:
{
  "0": "",
  "1": "92.60",
  "2": "96.80",
  "3": "103.20",
  "4": "103.80",
  "5": "90.30"
}
     */
}
