namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the state of the Somneo's FM radio
/// </summary>
public sealed class FMRadioState
{
    /// <summary>
    /// The current preset the frequency is set for. Can be between 1 and 5.
    /// </summary>
    public int Preset { get; }
    /// <summary>
    /// The current frequency of the FM radio.
    /// </summary>
    public float Frequency { get; }

    internal FMRadioState(
        int preset,
        float frequency)
    {
        Preset = preset;
        Frequency = frequency;
    }
}
