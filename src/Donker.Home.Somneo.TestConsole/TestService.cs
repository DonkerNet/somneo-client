using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private readonly OrderedDictionary _commands;

        private bool _canRun;

        public TestService(ISomneoApiClient somneoApiClient)
        {
            _somneoApiClient = somneoApiClient;
            
            _commands = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);

            RegisterCommand("help", "Show available commands.", ShowHelp);

            RegisterCommand("device", "Show the device information.", ShowDeviceDetails);
            RegisterCommand("firmware", "Show the firmware information.", ShowFirmwareDetails);
            RegisterCommand("wifi", "Show the wifi connection details.", ShowWifiDetails);
            RegisterCommand("locale", "Show the locale set for the device.", ShowLocale);
            RegisterCommand("time", "Show the time of the device.", ShowTime);

            RegisterCommand("sensor", "Show sensor data.", ShowSensorData);

            RegisterCommand("light", "Show the light state.", ShowLightState);
            RegisterCommand("toggle-light","[on/off]", "Toggle the light.", ToggleLight);
            RegisterCommand("set-light-level", "[1-25]", "Set the light level.", SetLightLevel);
            RegisterCommand("toggle-night-light", "[on/off]", "Toggle the night light.", ToggleNightLight);
            RegisterCommand("toggle-sunrise-preview", "[on/off]", "Toggle the Sunrise Preview mode.", ToggleSunrisePreview);

            RegisterCommand("display", "Show the display state.", ShowDisplayState);
            RegisterCommand("toggle-permanent-display", "[on/off]", "Toggle the permanent display.", TogglePermanentDisplay);
            RegisterCommand("set-display-level", "[1-6]", "Set the display level.", SetDisplayLevel);

            RegisterCommand("fm-radio-presets", "Show the FM-radio presets.", ShowFMRadioPresets);
            RegisterCommand("set-fm-radio-preset", $"[1-5] [{87.50F:0.00}-{107.99F:0.00}]", "Set an FM-radio preset to a frequency.", SetFMRadioPreset);
            RegisterCommand("fm-radio", "Show the FM-radio state.", ShowFMRadioState);
            RegisterCommand("enable-fm-radio", "Enable the FM-radion.", EnableFMRadio);
            RegisterCommand("enable-fm-radio-preset", "[1-5]", "Enable an FM-radio preset.", EnableFMRadioPreset);
            RegisterCommand("seek-fm-radio-station", "[up/down]", "Seek a next FM-radio station.", SeekFMRadioStation);

            RegisterCommand("player", "Show the player state.", ShowPlayerState);
            RegisterCommand("set-player-volume", "[1-25]", "Set the player volume.", SetPlayerVolume);
            RegisterCommand("disable-player", "Disable the player.", DisablePlayer);

            RegisterCommand("alarms", "Show the alarms.", ShowAlarms);

            RegisterCommand("exit", "Exit the application.", Exit);
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

        private void RegisterCommand(string commandName, string argumentsDescription, string description, Action<string> commandHandler)
        {
            CommandInfo command = new CommandInfo
            {
                Name = commandName,
                ArgumentsDescription = argumentsDescription,
                Description = description,
                Handler = commandHandler
            };

            _commands.Add(commandName, command);
        }

        private void RegisterCommand(string commandName, string description, Action<string> commandHandler)
        {
            RegisterCommand(commandName, null, description, commandHandler);
        }

        private CommandInfo GetCommandInfo(string commandName)
        {
            if (_commands.Contains(commandName))
                return _commands[commandName] as CommandInfo;
            return null;
        }

        private void RunCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return;

            string[] parts = command.Split(new[] { ' ' }, 2);

            var commandInfo = GetCommandInfo(parts[0]);

            if (commandInfo == null)
            {
                Console.WriteLine($"Invalid input: \"{command}\"");
                return;
            }

            string commandHandlerArguments = parts.Length > 1 ? parts[1] : null;
            
            try
            {
                commandInfo.Handler(commandHandlerArguments);
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

        #region Command handler methods

        private void ShowHelp(string args)
        {
            Console.WriteLine("Available commands:");

            const int pageSize = 8;
            int commandCount = _commands.Count;
            int counter = 0;

            foreach (CommandInfo commandInfo in _commands.Values)
            {
                Console.WriteLine(@$"
{commandInfo.Name} {commandInfo.ArgumentsDescription}
  {commandInfo.Description}");

                ++counter;

                if (counter % pageSize == 0 && counter < commandCount)
                {
                    Console.WriteLine(@$"
{counter}/{commandCount} command(s) shown. [C]ontinue or [S]top.");

                    bool requestInput = true;

                    while (requestInput)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.C:
                                requestInput = false;
                                break;
                            case ConsoleKey.S:
                                Console.WriteLine("Command list stopped.");
                                return;
                        }
                    }

                    Console.WriteLine("Available commands (continued):");
                }
            }

            Console.WriteLine(@"
Command list finished.");
        }

        private void ShowDeviceDetails(string args)
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

        private void ShowFirmwareDetails(string args)
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

        private void ShowWifiDetails(string args)
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

        private void ShowLocale(string args)
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

        private void ShowTime(string args)
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

        private void ShowSensorData(string args)
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

        private void ShowLightState(string args)
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

        private void ShowDisplayState(string args)
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

        private void ShowFMRadioPresets(string args)
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

        private void ShowFMRadioState(string args)
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

        private void EnableFMRadio(string args)
        {
            _somneoApiClient.EnableFMRadio();
            Console.WriteLine("FM radio enabled for the current preset.");
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

        private void SeekFMRadioStation(string args)
        {
            switch (args?.ToLower())
            {
                case "up":
                    _somneoApiClient.SeekFMRadioStation(RadioSeekDirection.Up);
                    Console.WriteLine("Seeking for a new FM radio station in forward direction.");
                    break;

                case "down":
                    _somneoApiClient.SeekFMRadioStation(RadioSeekDirection.Down);
                    Console.WriteLine("Seeking for a new FM radio station in backward direction.");
                    break;

                default:
                    Console.WriteLine("Specify \"up\" or \"down\".");
                    break;
            }
        }

        private void ShowPlayerState(string args)
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

        private void DisablePlayer(string args)
        {
            _somneoApiClient.DisablePlayer();
            Console.WriteLine("Audio player disabled.");
        }

        private void ShowAlarms(string args)
        {
            IReadOnlyList<Alarm> alarms = _somneoApiClient.GetAlarms();

            if (alarms.Count == 0)
            {
                Console.WriteLine("0/16 alarms set.");
                return;
            }

            StringBuilder consoleMessageBuilder = new StringBuilder();

            consoleMessageBuilder.Append($@"{alarms.Count}/16 alarm(s) set:");

            foreach (Alarm alarm in alarms.OrderBy(a => a.Hour).ThenBy(a => a.Minute))
            {
                consoleMessageBuilder.AppendFormat(@"
  {0} {1:00}:{2:00}{3} (PowerWake: {4})",
alarm.Enabled ? "OFF:" : "ON: ",
alarm.Hour,
alarm.Minute,
alarm.RepeatDays.Count > 0 ? " " + string.Join(",", alarm.RepeatDays.Select(d => string.Concat(d.ToString().Take(3)))) : string.Empty,
alarm.PowerWakeEnabled ? $"{alarm.PowerWakeHour.Value:00}:{alarm.PowerWakeMinute.Value:00}" : "off");
            }

            Console.WriteLine(consoleMessageBuilder);
        }

        private void Exit(string args) => _canRun = false;

        #endregion
    }
}
