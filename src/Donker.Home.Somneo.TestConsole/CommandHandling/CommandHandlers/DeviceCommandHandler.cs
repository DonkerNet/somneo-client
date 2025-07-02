using Donker.Home.Somneo.ApiClient;
using System.Text;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class DeviceCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("device", "Show the device information.", ShowDeviceDetailsAsync);
        commandRegistry.RegisterCommand("firmware", "Show the firmware information.", ShowFirmwareDetailsAsync);
        commandRegistry.RegisterCommand("wifi", "Show the wifi connection details.", ShowWifiDetailsAsync);
        commandRegistry.RegisterCommand("locale", "Show the locale set for the device.", ShowLocaleAsync);
        commandRegistry.RegisterCommand("time", "Show the time of the device.", ShowTimeAsync);
    }

    public async Task ShowDeviceDetailsAsync(string? args)
    {
        var deviceDetails = await SomneoApiClient.GetDeviceDetailsAsync();

        Console.WriteLine(
$@"Device details:
  Assigned name: {deviceDetails.AssignedName}
  Type number: {deviceDetails.TypeNumber}
  Serial number: {deviceDetails.Serial}
  Product ID: {deviceDetails.ProductId}
  Product name: {deviceDetails.ProductName}
  Model ID: {deviceDetails.ModelId}");
    }

    public async Task ShowFirmwareDetailsAsync(string? args)
    {
        var firmwareDetails = await SomneoApiClient.GetFirmwareDetailsAsync();

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

    public async Task ShowWifiDetailsAsync(string? args)
    {
        var wifiDetails = await SomneoApiClient.GetWifiDetailsAsync();

        Console.WriteLine(
$@"Wifi details:
  SSID: {wifiDetails.SSID}
  Protection: {wifiDetails.Protection}
  IP address: {wifiDetails.IPAddress}
  Netmask: {wifiDetails.Netmask}
  Gateway IP: {wifiDetails.Gateway}
  MAC address: {wifiDetails.MACAddress}");
    }

    public async Task ShowLocaleAsync(string? args)
    {
        var locale = await SomneoApiClient.GetLocaleAsync();

        Console.WriteLine(
$@"Locale:
  Country: {locale.Country}
  Timezone: {locale.Timezone}");
    }

    public async Task ShowTimeAsync(string? args)
    {
        var time = await SomneoApiClient.GetTimeAsync();

        Console.WriteLine(
$@"Time:
  Date/time: {time.DateTime}
  Timezone offset: {time.TimezoneOffset:\+hh\:mm}
  DST: {(time.IsDSTApplied ? "Yes" : "No")} (current offset = {time.CurrentDSTOffset:\+hh\:mm})
  Next DST change: {time.DSTChangeOver}");
    }
}
