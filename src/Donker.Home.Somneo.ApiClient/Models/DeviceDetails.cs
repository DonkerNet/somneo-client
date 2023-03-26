﻿using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the details of a Somneo device.
/// </summary>
public sealed class DeviceDetails
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// The name of the device.
    /// </summary>
    [JsonPropertyName("name")]
    public string AssignedName { get; init; }
    /// <summary>
    /// The type number of the device.
    /// </summary>
    [JsonPropertyName("ctn")]
    public string TypeNumber { get; init; }
    /// <summary>
    /// The serial number of the device.
    /// </summary>
    public string Serial { get; init; }
    /// <summary>
    /// The product ID of the device.
    /// </summary>
    public string ProductId { get; init; }
    /// <summary>
    /// The product name of the device.
    /// </summary>
    public string ProductName { get; init; }
    /// <summary>
    /// The model ID of the device.
    /// </summary>
    public string ModelId { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /* Example JSON:
{
  "name": "Name given to the Somneo device",
  "type": "HF367x",
  "modelid": "123456789012",
  "serial": "ABCD1234567890",
  "ctn": "HF3672/01",
  "allowuploads": true,
  "allowpairing": false,
  "wificountry": "NL/1",
  "swverwifi": "4.8.3.0",
  "cnversion": "4.8.3.0",
  "productid": "1234567890123B",
  "pkgver": 1113,
  "swveruictrl": "R1.56.000.PRD",
  "swverlight": "31",
  "swvermp3": "MP 0.0.1",
  "swvericons": "IC 0.0.11",
  "productname": "Wake-up Light"
}
     */
}
