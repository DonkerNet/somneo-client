using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the display state for the Somneo device.
/// </summary>
public sealed class DisplayState
{
    /// <summary>
    /// Whether the display is permanently shown or automatically disables after a period of time.
    /// </summary>
    [JsonPropertyName("dspon")]
    public bool Permanent { get; init; }
    /// <summary>
    /// The brightness level of the display.
    /// </summary>
    [JsonPropertyName("brght")]
    public int Brightness { get; init; }
}
