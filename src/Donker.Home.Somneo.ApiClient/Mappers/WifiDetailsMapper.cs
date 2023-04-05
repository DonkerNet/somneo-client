using Donker.Home.Somneo.ApiClient.Dto;
using Donker.Home.Somneo.ApiClient.Models;
using System.Net;

namespace Donker.Home.Somneo.ApiClient.Mappers;

internal class WifiDetailsMapper
{
    public static WifiDetails ToModel(WifiDetailsDto dto)
    {
        return new WifiDetails(
            dto.SSID,
            dto.Protection,
            IPAddress.Parse(dto.IPAddress),
            IPAddress.Parse(dto.Netmask),
            IPAddress.Parse(dto.Gateway),
            dto.MACAddress);
    }
}
