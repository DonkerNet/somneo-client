namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the locale set for the Somneo device.
/// </summary>
public sealed class Locale
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// The country set for the device.
    /// </summary>
    public string Country { get; init; }
    /// <summary>
    /// The timezone set for the device.
    /// </summary>
    public string Timezone { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
