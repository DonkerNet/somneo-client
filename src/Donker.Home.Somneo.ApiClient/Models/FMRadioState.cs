using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the state of the Somneo's FM radio
/// </summary>
public sealed class FMRadioState
{
    /// <summary>
    /// The current frequency of the FM radio.
    /// </summary>
    [JsonPropertyName("fmfrq")]
    public float Frequency { get; init; }
    /// <summary>
    /// The current preset the frequency is set for. Can be between 1 and 5.
    /// </summary>
    [JsonPropertyName("prstn")]
    public int Preset { get; init; }
}
