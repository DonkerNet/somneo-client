using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// A collection of FM radio frequency presents.
/// </summary>
public sealed class FMRadioPresets
{
    /// <summary>
    /// The frequency of the first preset.
    /// </summary>
    [JsonPropertyName("1")]
    public float Preset1 { get; init; }
    /// <summary>
    /// The frequency of the second preset.
    /// </summary>
    [JsonPropertyName("2")]
    public float Preset2 { get; init; }
    /// <summary>
    /// The frequency of the third preset.
    /// </summary>
    [JsonPropertyName("3")]
    public float Preset3 { get; init; }
    /// <summary>
    /// The frequency of the fourth preset.
    /// </summary>
    [JsonPropertyName("4")]
    public float Preset4 { get; init; }
    /// <summary>
    /// The frequency of the fifth preset.
    /// </summary>
    [JsonPropertyName("5")]
    public float Preset5 { get; init; }

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
