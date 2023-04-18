using System.Text;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class DeviceCommandHandler : CommandHandlerBase
{
    public DeviceCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("device", "Show the device information.", ShowDeviceDetails);
        commandRegistry.RegisterCommand("firmware", "Show the firmware information.", ShowFirmwareDetails);
        commandRegistry.RegisterCommand("wifi", "Show the wifi connection details.", ShowWifiDetails);
        commandRegistry.RegisterCommand("locale", "Show the locale set for the device.", ShowLocale);
        commandRegistry.RegisterCommand("time", "Show the time of the device.", ShowTime);
    }

    public void ShowDeviceDetails(string? args)
    {
        DeviceDetails deviceDetails = SomneoApiClient.GetDeviceDetails();

        Console.WriteLine(
$@"Device details:
  Assigned name: {deviceDetails.AssignedName}
  Type number: {deviceDetails.TypeNumber}
  Serial number: {deviceDetails.Serial}
  Product ID: {deviceDetails.ProductId}
  Product name: {deviceDetails.ProductName}
  Model ID: {deviceDetails.ModelId}");
    }

    public void ShowFirmwareDetails(string? args)
    {
        FirmwareDetails firmwareDetails = SomneoApiClient.GetFirmwareDetails();

        var consoleMessageBuilder = new StringBuilder();

        consoleMessageBuilder.Append(
$@"Firmware details:
  Name: {firmwareDetails.Name}
  Version: {firmwareDetails.Version}
  Can download: {(firmwareDetails.CanDownload ? "Yes" : "No")}
  Can upgrade: {(firmwareDetails.CanUpgrade ? "Yes" : "No")}
  Upgrade: {firmwareDetails.Upgrade}
  Mandatory: {(firmwareDetails.Mandatory ? "Yes" : "No")}
  State: {firmwareDetails.State}
  Update progress: {firmwareDetails.Progress}
  Status message: {firmwareDetails.StatusMessage}");

        Console.WriteLine(consoleMessageBuilder);
    }

    public void ShowWifiDetails(string? args)
    {
        WifiDetails wifiDetails = SomneoApiClient.GetWifiDetails();

        Console.WriteLine(
$@"Wifi details:
  SSID: {wifiDetails.SSID}
  Protection: {wifiDetails.Protection}
  IP address: {wifiDetails.IPAddress}
  Netmask: {wifiDetails.Netmask}
  Gateway IP: {wifiDetails.Gateway}
  MAC address: {wifiDetails.MACAddress}");
    }

    public void ShowLocale(string? args)
    {
        Locale locale = SomneoApiClient.GetLocale();

        Console.WriteLine(
$@"Locale:
  Country: {locale.Country}
  Timezone: {locale.Timezone}");
    }

    public void ShowTime(string? args)
    {
        Time time = SomneoApiClient.GetTime();

        Console.WriteLine(
$@"Time:
  Date/time: {time.DateTime}
  Timezone offset: {time.TimezoneOffset:\+hh\:mm}
  DST: {(time.IsDSTApplied ? "Yes" : "No")} (current offset = {time.CurrentDSTOffset:\+hh\:mm})
  Next DST change: {time.DSTChangeOver}");
    }
}
