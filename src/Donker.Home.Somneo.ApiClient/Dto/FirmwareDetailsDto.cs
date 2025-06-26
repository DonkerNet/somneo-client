using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class FirmwareDetailsDto
{
    public required string Name { get; set; }

    public required string Version { get; set; }

    public required string State { get; set; }

    public string? Upgrade { get; set; }

    public int Progress { get; set; }

    [JsonPropertyName("statusmsg")]
    public string? StatusMessage { get; set; }

    public bool CanUpgrade { get; set; }

    public bool CanDownload { get; set; }

    public bool Mandatory { get; set; }

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
