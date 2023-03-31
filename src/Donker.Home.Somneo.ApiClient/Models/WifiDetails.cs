using System.Net;

namespace Donker.Home.Somneo.ApiClient.Models;

/// <summary>
/// Describes the details of the wifi connection of a Somneo device.
/// </summary>
public sealed class WifiDetails
{
    /// <summary>
    /// The SSID of the network the device is connected to.
    /// </summary>
    public string SSID { get; }
    /// <summary>
    /// The type of protection used for the wifi connection.
    /// </summary>
    public string Protection { get; }
    /// <summary>
    /// The IP address assigned to the device.
    /// </summary>
    public IPAddress IPAddress { get; }
    /// <summary>
    /// The netmask of the network the device is connected to.
    /// </summary>
    public IPAddress Netmask { get; }
    /// <summary>
    /// The gateway of the network the device is connected to.
    /// </summary>
    public IPAddress Gateway { get; }
    /// <summary>
    /// The MAC address of the device.
    /// </summary>
    public string MACAddress { get; }

    internal WifiDetails(
        string ssid,
        string protection,
        IPAddress ipAddress,
        IPAddress netmask,
        IPAddress gateway,
        string macAddress)
    {
        SSID = ssid;
        Protection = protection;
        IPAddress = ipAddress;
        Netmask = netmask;
        Gateway = gateway;
        MACAddress = macAddress;
    }
}
