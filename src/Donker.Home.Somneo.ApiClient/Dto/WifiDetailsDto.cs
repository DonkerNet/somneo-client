namespace Donker.Home.Somneo.ApiClient.Dto;

internal class WifiDetailsDto
{
    public required string SSID { get; set; }

    public required string Protection { get; set; }

    public required string IPAddress { get; set; }

    public required string Netmask { get; set; }

    public required string Gateway { get; set; }

    public required string MACAddress { get; set; }

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
