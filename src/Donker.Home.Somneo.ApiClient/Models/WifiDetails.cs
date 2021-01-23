using System.Net;
using Donker.Home.Somneo.ApiClient.Serialization.Converters;
using Newtonsoft.Json;

namespace Donker.Home.Somneo.ApiClient.Models
{
    /// <summary>
    /// Describes the details of the wifi connection of a Somneo device.
    /// </summary>
    public sealed class WifiDetails
    {
        /// <summary>
        /// The SSID of the network the device is connected to.
        /// </summary>
        public string SSID { get; init; }
        /// <summary>
        /// The type of protection used for the wifi connection.
        /// </summary>
        public string Protection { get; init; }
        /// <summary>
        /// The IP address assigned to the device.
        /// </summary>
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress IPAddress { get; init; }
        /// <summary>
        /// The netmask of the network the device is connected to.
        /// </summary>
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress Netmask { get; init; }
        /// <summary>
        /// The gateway of the network the device is connected to.
        /// </summary>
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress Gateway { get; init; }
        /// <summary>
        /// The MAC address of the device.
        /// </summary>
        public string MACAddress { get; set; }
    }
}
