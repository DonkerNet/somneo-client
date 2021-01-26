using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Helpers;
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

            RegisterCommand("light-state", args => GetLightState());
            RegisterCommand("toggle-light", args => ToggleLight(args));
            RegisterCommand("set-light-level", args => SetLightLevel(args));
            RegisterCommand("toggle-night-light", args => ToggleNightLight(args));
            RegisterCommand("toggle-sunrise-preview", args => ToggleSunrisePreview(args));

            RegisterCommand("display-state", args => GetDisplayState());
            RegisterCommand("toggle-permanent-display", args => TogglePermanentDisplay(args));
            RegisterCommand("set-display-level", args => SetDisplayLevel(args));

            RegisterCommand("fm-radio-presets", args => GetFMRadioPresets());
            RegisterCommand("set-fm-radio-preset", args => SetFMRadioPreset(args));
            RegisterCommand("fm-radio-state", args => GetFMRadioState());
            RegisterCommand("enable-fm-radio", args => EnableFMRadio());
            RegisterCommand("enable-fm-radio-preset", args => EnableFMRadioPreset(args));

            RegisterCommand("player-state", args => GetPlayerState());
            RegisterCommand("set-player-volume", args => SetPlayerVolume(args));
            RegisterCommand("disable-player", args => DisablePlayer());

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

        private static void ShowHelp()
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
    light-state
    toggle-light [on/off]
    set-light-level [1-25]
    toggle-night-light [on/off]
    toggle-sunrise-preview [on/off]
    ----
    display-state
    toggle-permanent-display [on/off]
    set-display-level [1-6]
    ----
    fm-radio-presets
    set-fm-radio-preset [1-5] [{87.50F:0.00}-{107.99F:0.00}]
    fm-radio-state
    enable-fm-radio
    enable-fm-radio-preset [1-5]
    ----
    player-state
    set-player-volume [1-25]
    disable-player
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
$@"Device details:
  Name: {deviceDetails.Name}
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

        private void GetWifiDetails()
        {
            WifiDetails wifiDetails = _somneoApiClient.GetWifiDetails();

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

        private void GetLocale()
        {
            Locale locale = _somneoApiClient.GetLocale();

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

        private void GetTime()
        {
            Time time = _somneoApiClient.GetTime();

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

        private void GetSensorData()
        {
            SensorData sensorData = _somneoApiClient.GetSensorData();

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

        private void GetLightState()
        {
            LightState lightState = _somneoApiClient.GetLightState();

            if (lightState == null)
            {
                Console.WriteLine("Unable to retrieve the light state.");
                return;
            }

            Console.WriteLine(
$@"Light state:
  Normal light enabled: {(lightState.LightEnabled ? "Yes" : "No")}
  Light level: {lightState.LightLevel}/25
  Night light enabled: {(lightState.NightLightEnabled ? "Yes" : "No")}
  Sunrise preview enabled: {(lightState.SunrisePreviewEnabled ? "Yes" : "No")}");
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

        private void GetDisplayState()
        {
            DisplayState displayState = _somneoApiClient.GetDisplayState();

            if (displayState == null)
            {
                Console.WriteLine("Unable to retrieve the display state.");
                return;
            }

            Console.WriteLine(
$@"Display state:
  Permanent display enabled: {(displayState.Permanent ? "Yes" : "No")}
  Brightness level: {displayState.Brightness}/6");
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

        private void GetFMRadioPresets()
        {
            FMRadioPresets fmRadioPresets = _somneoApiClient.GetFMRadioPresets();

            if (fmRadioPresets == null)
            {
                Console.WriteLine("Unable to retrieve the FM radio presets.");
                return;
            }

            Console.WriteLine(
$@"FM radio presets:
  1: {fmRadioPresets.Preset1:0.00} FM
  2: {fmRadioPresets.Preset2:0.00} FM
  3: {fmRadioPresets.Preset3:0.00} FM
  4: {fmRadioPresets.Preset4:0.00} FM
  5: {fmRadioPresets.Preset5:0.00} FM");
        }

        private void SetFMRadioPreset(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
                string[] argsArray = args.Split(new[] { ' ' }, 2);

                if (argsArray.Length == 2
                    && int.TryParse(argsArray[0], out int position)
                    && position >= 1 && position <= 5
                    && float.TryParse(argsArray[1], out float frequency)
                    && frequency >= 87.50 && frequency <= 177.99)
                {
                    _somneoApiClient.SetFMRadioPreset(position, frequency);
                    Console.WriteLine($"Preset {position} set to {frequency:0.00} FM.");
                    return;
                }
            }

            Console.WriteLine("Specify a position between 1 and 5, followed by a frequency between 87.50 and 107.99.");
        }

        private void GetFMRadioState()
        {
            FMRadioState fmRadioState = _somneoApiClient.GetFMRadioState();

            if (fmRadioState == null)
            {
                Console.WriteLine("Unable to retrieve the FM radio state.");
                return;
            }

            Console.WriteLine(
$@"FM radio state:
  Frequency: {fmRadioState.Frequency:0.00} FM
  Preset: {fmRadioState.Preset}/5");
        }

        private void EnableFMRadio()
        {
            _somneoApiClient.EnableFMRadio();
            Console.WriteLine("FM radio enabled for the default preset.");
        }

        private void EnableFMRadioPreset(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int preset) && preset >= 1 && preset <= 5)
            {
                _somneoApiClient.EnableFMRadioPreset(preset);
                Console.WriteLine($"FM radio enabled for preset {preset}/5.");
                return;
            }

            Console.WriteLine("The preset should be between 1 and 5.");
        }

        private void GetPlayerState()
        {
            PlayerState playerState = _somneoApiClient.GetPlayerState();

            if (playerState == null)
            {
                Console.WriteLine("Unable to retrieve the audio player state.");
                return;
            }

            Console.WriteLine(
$@"Audio player state:
  Enabled: {(playerState.Enabled ? "Yes" : "No")}
  Volume: {playerState.Volume}/25
  Device: {EnumHelper.GetDescription(playerState.Device)}
  Channel/preset: {playerState.ChannelOrPreset}");
        }

        private void SetPlayerVolume(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int volume) && volume >= 1 && volume <= 25)
            {
                _somneoApiClient.SetPlayerVolume(volume);
                Console.WriteLine($"Audio player volume set to {volume}/25.");
                return;
            }

            Console.WriteLine("Specify a volume between 1 and 25.");
        }
        
        private void DisablePlayer()
        {
            _somneoApiClient.DisablePlayer();
            Console.WriteLine("Audio player disabled.");
        }
    }
}
