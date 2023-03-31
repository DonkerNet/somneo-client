using System.Text.Json.Serialization;

namespace Donker.Home.Somneo.ApiClient.Dto;

internal class DeviceDetailsDto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonPropertyName("name")]
    public string AssignedName { get; set; }

    [JsonPropertyName("ctn")]
    public string TypeNumber { get; set; }

    public string Serial { get; set; }

    public string ProductId { get; set; }

    public string ProductName { get; set; }

    public string ModelId { get; set; }
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
