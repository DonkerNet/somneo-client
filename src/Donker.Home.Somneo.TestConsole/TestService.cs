﻿using System;
using System.Collections.Specialized;
using System.Linq;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole
{
    public class TestService
    {
        private readonly ISomneoApiClient _somneoApiClient;
        private readonly OrderedDictionary _commandHandlers;

        private bool _canRun;

        public TestService(ISomneoApiClient somneoApiClient)
        {
            _somneoApiClient = somneoApiClient;
            
            _commandHandlers = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);

            RegisterCommand("help", args => ShowHelp());

            RegisterCommand("device-details", args => GetDeviceDetails());
            RegisterCommand("firmware-details", args => GetFirmwareDetails());
            RegisterCommand("wifi-details", args => GetWifiDetails());
            RegisterCommand("locale", args => GetLocale());
            RegisterCommand("time", args => GetTime());

            RegisterCommand("sensor-data", args => GetSensorData());

            RegisterCommand("light-settings", args => GetLightSettings());
            RegisterCommand("toggle-light", args => ToggleLight(args));
            RegisterCommand("set-light-level", args => SetLightLevel(args));
            RegisterCommand("toggle-night-light", args => ToggleNightLight(args));
            RegisterCommand("toggle-sunrise-preview", args => ToggleSunrisePreview(args));

            RegisterCommand("display-settings", args => GetDisplaySettings());
            RegisterCommand("toggle-permanent-display", args => TogglePermanentDisplay(args));
            RegisterCommand("set-display-level", args => SetDisplayLevel(args));

            RegisterCommand("exit", args => _canRun = false);
        }

        private void RegisterCommand(string commandName, Action<string> commandHandler)
        {
            _commandHandlers.Add(commandName, commandHandler);
        }

        private Action<string> GetCommandHandler(string commandName)
        {
            if (_commandHandlers.Contains(commandName))
                return _commandHandlers[commandName] as Action<string>;
            return null;
        }

        public void Run()
        {
            _canRun = true;

            Console.WriteLine(
@"Welcome to the Somneo API client test console.
Type ""help"" to get started.");

            do
            {
                Console.WriteLine();
                Console.Write("> ");
                string command = Console.ReadLine();
                Console.WriteLine();
                RunCommand(command);
            }
            while (_canRun);

            Console.WriteLine();
            Console.WriteLine("Bye!");
        }

        private void RunCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return;

            string[] parts = command.Split(new[] { ' ' }, 2);

            var commandHandler = GetCommandHandler(parts[0]);

            if (commandHandler == null)
            {
                Console.WriteLine($"Invalid input: \"{command}\"");
                return;
            }

            string commandHandlerArguments = parts.Length > 1 ? parts[1] : null;
            
            try
            {
                commandHandler(commandHandlerArguments);
            }
            catch (SomneoApiException ex)
            {
                Console.WriteLine("Somneo error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex);
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine(
$@"Available commands:
    help
    ----
    device-details
    firmware-details
    wifi-details
    locale
    time
    ----
    sensor-data
    ----
    light-settings
    toggle-light [on/off]
    set-light-level [1-25]
    toggle-night-light [on/off]
    toggle-sunrise-preview [on/off]
    ----
    display-settings
    toggle-permanent-display [on/off]
    set-display-level [1-6]
    ----
    exit");
        }

        private void GetDeviceDetails()
        {
            DeviceDetails deviceDetails = _somneoApiClient.GetDeviceDetails();

            if (deviceDetails == null)
            {
                Console.WriteLine("Unable to retrieve the device details.");
                return;
            }

            Console.WriteLine(
$@"Name: {deviceDetails.Name}
Model: {deviceDetails.Model}
Series: {deviceDetails.Series}
Product range: {deviceDetails.ProductRange}
Serial number: {deviceDetails.Serial}
Product ID: {deviceDetails.ProductId}");
        }

        private void GetFirmwareDetails()
        {
            FirmwareDetails firmwareDetails = _somneoApiClient.GetFirmwareDetails();

            if (firmwareDetails == null)
            {
                Console.WriteLine("Unable to retrieve the firmware details.");
                return;
            }

            Console.WriteLine(
$@"Name: {firmwareDetails.Name}
Version: {firmwareDetails.Version}
State: {firmwareDetails.State}");

            if (!firmwareDetails.IsIdle)
            {
                Console.WriteLine(
$@"Update progress: {firmwareDetails.Progress}
Status message: {firmwareDetails.StatusMessage}");
            }
            
            if (firmwareDetails.CanUpgrade)
            {
                Console.WriteLine(
$@"Available upgrade: {firmwareDetails.Upgrade}
Mandatory: {(firmwareDetails.Mandatory ? "Yes" : "No")}");
            }
        }

        private void GetWifiDetails()
        {
            WifiDetails wifiDetails = _somneoApiClient.GetWifiDetails();

            if (wifiDetails == null)
            {
                Console.WriteLine("Unable to retrieve the wifi details.");
                return;
            }

            Console.WriteLine(
$@"SSID: {wifiDetails.SSID}
Protection: {wifiDetails.Protection}
IP address: {wifiDetails.IPAddress}
Netmask: {wifiDetails.Netmask}
Gateway IP: {wifiDetails.Gateway}
MAC address: {wifiDetails.MACAddress}");
        }

        private void GetLocale()
        {
            Locale locale = _somneoApiClient.GetLocale();

            if (locale == null)
            {
                Console.WriteLine("Unable to retrieve the locale.");
                return;
            }

            Console.WriteLine(
$@"Country: {locale.Country}
Timezone: {locale.Timezone}");
        }

        private void GetTime()
        {
            Time time = _somneoApiClient.GetTime();

            if (time == null)
            {
                Console.WriteLine("Unable to retrieve the time.");
                return;
            }

            Console.WriteLine(
$@"Date/time: {time.DateTime}
Timezone offset: {time.TimezoneOffset}
DST: {(time.IsDSTApplied ? "Yes" : "No")} (offset = {time.DSTOffset})
Next DST change: {time.DSTChangeOver}");
        }

        private void GetSensorData()
        {
            SensorData sensorData = _somneoApiClient.GetSensorData();

            if (sensorData == null)
            {
                Console.WriteLine("Unable to retrieve the sensor data.");
                return;
            }

            Console.WriteLine(
$@"Temperature: {sensorData.CurrentTemperature} °C (avg: {sensorData.AverageTemperature} °C)
Light: {sensorData.CurrentLight} lux (avg: {sensorData.AverageLight} lux)
Sound: {sensorData.CurrentSound} dB (avg: {sensorData.AverageSound} dB)
Humidity: {sensorData.CurrentHumidity} % (avg: {sensorData.AverageHumidity} %)");
        }

        private void GetLightSettings()
        {
            LightSettings lightSettings = _somneoApiClient.GetLightSettings();

            if (lightSettings == null)
            {
                Console.WriteLine("Unable to retrieve the light settings.");
                return;
            }

            Console.WriteLine(
$@"Normal light enabled: {(lightSettings.LightEnabled ? "Yes" : "No")}
Light level: {lightSettings.LightLevel}/25
Night light enabled: {(lightSettings.NightLightEnabled ? "Yes" : "No")}
Sunrise preview enabled: {(lightSettings.SunrisePreviewEnabled ? "Yes" : "No")}");
        }

        private void ToggleLight(string args)
        {
            switch (args?.ToLower())
            {
                case "on":
                    _somneoApiClient.ToggleLight(true);
                    Console.WriteLine("Light enabled.");
                    break;

                case "off":
                    _somneoApiClient.ToggleLight(false);
                    Console.WriteLine("Light disabled.");
                    break;

                default:
                    Console.WriteLine("Specify \"on\" or \"off\".");
                    break;
            }
        }

        private void SetLightLevel(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int level) && level >= 1 && level <= 25)
            {
                _somneoApiClient.SetLightLevel(level);
                Console.WriteLine($"Light level set to {level}/25.");
                return;
            }

            Console.WriteLine("Specify a light level between 1 and 25.");
        }

        private void ToggleNightLight(string args)
        {
            switch (args?.ToLower())
            {
                case "on":
                    _somneoApiClient.ToggleNightLight(true);
                    Console.WriteLine("Night light enabled.");
                    break;

                case "off":
                    _somneoApiClient.ToggleNightLight(false);
                    Console.WriteLine("Night light disabled.");
                    break;

                default:
                    Console.WriteLine("Specify \"on\" or \"off\".");
                    break;
            }
        }

        private void ToggleSunrisePreview(string args)
        {
            switch (args?.ToLower())
            {
                case "on":
                    _somneoApiClient.ToggleSunrisePreview(true);
                    Console.WriteLine("Sunrise preview enabled.");
                    break;

                case "off":
                    _somneoApiClient.ToggleSunrisePreview(true);
                    Console.WriteLine("Sunrise preview disabled.");
                    break;

                default:
                    Console.WriteLine("Specify \"on\" or \"off\".");
                    break;
            }
        }

        private void GetDisplaySettings()
        {
            DisplaySettings displaySettings = _somneoApiClient.GetDisplaySettings();

            if (displaySettings == null)
            {
                Console.WriteLine("Unable to retrieve the display settings.");
                return;
            }

            Console.WriteLine(
$@"Permanent display enabled: {(displaySettings.Permanent ? "Yes" : "No")}
Brightness level: {displaySettings.Brightness}/6");
        }

        private void TogglePermanentDisplay(string args)
        {
            switch (args?.ToLower())
            {
                case "on":
                    _somneoApiClient.TogglePermanentDisplay(true);
                    Console.WriteLine("Permanent display enabled.");
                    break;

                case "off":
                    _somneoApiClient.TogglePermanentDisplay(false);
                    Console.WriteLine("Permanent display disabled.");
                    break;

                default:
                    Console.WriteLine("Specify \"on\" or \"off\".");
                    break;
            }
        }

        private void SetDisplayLevel(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int level) && level >= 1 && level <= 6)
            {
                _somneoApiClient.SetDisplayLevel(level);
                Console.WriteLine($"Display brightness level set to {level}/6.");
                return;
            }

            Console.WriteLine("Specify a light level between 1 and 6.");
        }
    }
}