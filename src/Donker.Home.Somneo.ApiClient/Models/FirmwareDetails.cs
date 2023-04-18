namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the firware the Somneo device is currently running.
/// </summary>
public sealed class FirmwareDetails
{
    /// <summary>
    /// The name of the firmware the device is running.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// The version of the firmware the device is running.
    /// </summary>
    public string Version { get; }
    /// <summary>
    /// The current state of the firware.
    /// </summary>
    public string State { get; }
    /// <summary>
    /// The firmware upgrade.
    /// </summary>
    public string? Upgrade { get; }
    /// <summary>
    /// The progress in case the firmware is currently being upgraded.
    /// </summary>
    public int Progress { get; }
    /// <summary>
    /// Describes the status in case a firmware upgrade is in progress.
    /// </summary>
    public string? StatusMessage { get; }
    /// <summary>
    /// Whether the firmware can be upgraded or not.
    /// </summary>
    public bool CanUpgrade { get; }
    /// <summary>
    /// Whether firmware can be downloaded or not.
    /// </summary>
    public bool CanDownload { get; }
    /// <summary>
    /// Whether the available firmware upgrade is mandatory or not.
    /// </summary>
    public bool Mandatory { get; }

    internal FirmwareDetails(
        string name,
        string version,
        string state,
        string? upgrade,
        int progress,
        string? statusMessage,
        bool canUpgrade,
        bool canDownload,
        bool mandatory)
    {
        Name = name;
        Version = version;
        State = state;
        Upgrade = upgrade;
        Progress = progress;
        StatusMessage = statusMessage;
        CanUpgrade = canUpgrade;
        CanDownload = canDownload;
        Mandatory = mandatory;
    }
}
