namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the display state for the Somneo device.
/// </summary>
public sealed class DisplayState
{
    /// <summary>
    /// Whether the display is permanently shown or automatically disables after a period of time.
    /// </summary>
    public bool Permanent { get; }
    /// <summary>
    /// The brightness level of the display.
    /// </summary>
    public int Brightness { get; }

    internal DisplayState(
        bool permanent,
        int brightness)
    {
        Permanent = permanent;
        Brightness = brightness;
    }
}
