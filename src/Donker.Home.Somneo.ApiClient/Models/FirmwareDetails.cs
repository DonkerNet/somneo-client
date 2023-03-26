using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the firware the Somneo device is currently running.
/// </summary>
public sealed class FirmwareDetails
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// The name of the firmware the device is running.
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// The version of the firmware the device is running.
    /// </summary>
    public string Version { get; init; }
    /// <summary>
    /// The current state of the firware.
    /// </summary>
    public string State { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// The firmware upgrade.
    /// </summary>
    public string? Upgrade { get; init; }
    /// <summary>
    /// The progress in case the firmware is currently being upgraded.
    /// </summary>
    public int Progress { get; init; }
    /// <summary>
    /// Describes the status in case a firmware upgrade is in progress.
    /// </summary>
    [JsonPropertyName("statusmsg")]
    public string? StatusMessage { get; init; }
    /// <summary>
    /// Whether the firmware can be upgraded or not.
    /// </summary>
    public bool CanUpgrade { get; init; }
    /// <summary>
    /// Whether firmware can be downloaded or not.
    /// </summary>
    public bool CanDownload { get; init; }
    /// <summary>
    /// Whether the available firmware upgrade is mandatory or not.
    /// </summary>
    public bool Mandatory { get; init; }

    /* Example JSON:
{
  "name": "BE32-PROD-WF",
  "version": "1113",
  "versions": {
    "cn": "1113"
  },
  "upgrade": "",
  "state": "idle",
  "progress": 0,
  "statusmsg": "",
  "mandatory": false,
  "canupgrade": false,
  "candownload": true,
  "maxchunksize": 512,
  "size": 0,
  "data": "",
  "request": ""
}
     */
}
