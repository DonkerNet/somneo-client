namespace Donker.Home.Somneo.ApiClient.Dto;

internal class WifiDetailsDto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string SSID { get; set; }

    public string Protection { get; set; }

    public string IPAddress { get; set; }

    public string Netmask { get; set; }

    public string Gateway { get; set; }

    public string MACAddress { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /* Example JSON:
{
  "ssid": "SSID",
  "password": "",
  "protection": "wpa-2",
  "ipaddress": "192.168.0.123",
  "netmask": "255.255.255.0",
  "gateway": "192.168.0.1",
  "dhcp": true,
  "macaddress": "a1:b2:c3:d4:f5:ab",
  "cppid": "a1b2c3d4f5ab",
  "travelssid": "",
  "travelpassword": ""
}
     */
}
