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

        if (deviceDetails == null)
        {
            Console.WriteLine("Unable to retrieve the device details.");
            return;
        }

        Console.WriteLine(
$@"Device details:
  Name: {deviceDetails.Name}
  Model: {deviceDetails.Model}
  Series: {deviceDetails.Series}
  Product range: {deviceDetails.ProductRange}
  Serial number: {deviceDetails.Serial}
  Product ID: {deviceDetails.ProductId}");
    }

    public void ShowFirmwareDetails(string? args)
    {
        FirmwareDetails firmwareDetails = SomneoApiClient.GetFirmwareDetails();

        if (firmwareDetails == null)
        {
            Console.WriteLine("Unable to retrieve the firmware details.");
            return;
        }

        StringBuilder consoleMessageBuilder = new StringBuilder();

        consoleMessageBuilder.Append(
$@"Firmware details:
  Name: {firmwareDetails.Name}
  Version: {firmwareDetails.Version}
  State: {firmwareDetails.State}");

        if (!firmwareDetails.IsIdle)
        {
            consoleMessageBuilder.Append(
$@"
  Update progress: {firmwareDetails.Progress}
  Status message: {firmwareDetails.StatusMessage}");
        }

        if (firmwareDetails.CanUpgrade)
        {
            consoleMessageBuilder.Append(
$@"
  Available upgrade: {firmwareDetails.Upgrade}
  Mandatory: {(firmwareDetails.Mandatory ? "Yes" : "No")}");
        }

        Console.WriteLine(consoleMessageBuilder);
    }

    public void ShowWifiDetails(string? args)
    {
        WifiDetails wifiDetails = SomneoApiClient.GetWifiDetails();

        if (wifiDetails == null)
        {
            Console.WriteLine("Unable to retrieve the wifi details.");
            return;
        }

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

        if (locale == null)
        {
            Console.WriteLine("Unable to retrieve the locale.");
            return;
        }

        Console.WriteLine(
$@"Locale:
  Country: {locale.Country}
  Timezone: {locale.Timezone}");
    }

    public void ShowTime(string? args)
    {
        Time time = SomneoApiClient.GetTime();

        if (time == null)
        {
            Console.WriteLine("Unable to retrieve the time.");
            return;
        }

        Console.WriteLine(
$@"Time:
  Date/time: {time.DateTime}
  Timezone offset: {time.TimezoneOffset}
  DST: {(time.IsDSTApplied ? "Yes" : "No")} (offset = {time.DSTOffset})
  Next DST change: {time.DSTChangeOver}");
    }

    public void ShowSensorData(string? args)
    {
        SensorData sensorData = SomneoApiClient.GetSensorData();

        if (sensorData == null)
        {
            Console.WriteLine("Unable to retrieve the sensor data.");
            return;
        }

        Console.WriteLine(
$@"Sensor data:
  Temperature: {sensorData.CurrentTemperature} °C (avg: {sensorData.AverageTemperature} °C)
  Light: {sensorData.CurrentLight} lux (avg: {sensorData.AverageLight} lux)
  Sound: {sensorData.CurrentSound} dB (avg: {sensorData.AverageSound} dB)
  Humidity: {sensorData.CurrentHumidity} % (avg: {sensorData.AverageHumidity} %)");
    }
}
