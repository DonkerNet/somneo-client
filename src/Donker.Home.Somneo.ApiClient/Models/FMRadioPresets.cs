namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// A collection of FM radio frequency presents.
/// </summary>
public sealed class FMRadioPresets
{
    /// <summary>
    /// The frequency of the first preset.
    /// </summary>
    public float Preset1 { get; }
    /// <summary>
    /// The frequency of the second preset.
    /// </summary>
    public float Preset2 { get; }
    /// <summary>
    /// The frequency of the third preset.
    /// </summary>
    public float Preset3 { get; }
    /// <summary>
    /// The frequency of the fourth preset.
    /// </summary>
    public float Preset4 { get; }
    /// <summary>
    /// The frequency of the fifth preset.
    /// </summary>
    public float Preset5 { get; }

    internal FMRadioPresets(
        float preset1,
        float preset2,
        float preset3,
        float preset4,
        float preset5)
    {
        Preset1 = preset1;
        Preset2 = preset2;
        Preset3 = preset3;
        Preset4 = preset4;
        Preset5 = preset5;
    }
}
