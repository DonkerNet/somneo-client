using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.DemoApp.ApiModels.Demo;

public class DetailsResponseModel
{
    public DeviceDetails Device { get; }
    public WifiDetails Wifi { get; }
    public FirmwareDetails Firmware { get; }
    public Locale Locale { get; }
    public Time Time { get; }

    public DetailsResponseModel(
        DeviceDetails device,
        WifiDetails wifi,
        FirmwareDetails firmware,
        Locale locale,
        Time time)
    {
        Device = device;
        Wifi = wifi;
        Firmware = firmware;
        Locale = locale;
        Time = time;
    }
}
