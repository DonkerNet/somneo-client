using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers
{
    public class AlarmCommandHandler : CommandHandlerBase
    {
        public AlarmCommandHandler(ISomneoApiClient somneoApiClient)
            : base(somneoApiClient)
        {
        }

        public override void RegisterCommands(CommandRegistry commandRegistry)
        {
            commandRegistry.RegisterCommand("alarms", "Show the alarms.", ShowAlarms);
            commandRegistry.RegisterCommand("alarm-settings", "[1-16]", "Show the settings of an alarm.", ShowAlarmSettings);
            commandRegistry.RegisterCommand("toggle-alarm", "[1-16] [on/off]", "Toggle an alarm.", ToggleAlarm);
            commandRegistry.RegisterCommand("remove-alarm", "[1-16]", "Remove an alarm.", RemoveAlarm);
            commandRegistry.RegisterCommand("set-snooze-time", "[1-20]", "Sets the snooze time in minutes.", SetSnoozeTime);
        }

        private void ShowAlarms(string args)
        {
            IReadOnlyList<Alarm> alarms = SomneoApiClient.GetAlarms();

            if (alarms.Count == 0)
            {
                Console.WriteLine("0/16 alarms set.");
                return;
            }

            StringBuilder consoleMessageBuilder = new StringBuilder();

            consoleMessageBuilder.Append($@"{alarms.Count}/16 alarm(s) set:");

            foreach (Alarm alarm in alarms)
            {
                consoleMessageBuilder.AppendFormat(@"
  #{0} {1} {2:00}:{3:00}{4} (PowerWake: {5})",
alarm.Position,
alarm.Enabled ? "ON: " : "OFF:",
alarm.Hour,
alarm.Minute,
alarm.RepeatDays.Count > 0 ? " " + string.Join(",", alarm.RepeatDays.Select(d => string.Concat(d.ToString().Take(3)))) : string.Empty,
alarm.PowerWakeEnabled ? $"{alarm.PowerWakeHour.Value:00}:{alarm.PowerWakeMinute.Value:00}" : "off");
            }

            Console.WriteLine(consoleMessageBuilder);
        }

        private void ShowAlarmSettings(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int position) && position >= 1 && position <= 16)
            {
                AlarmSettings alarmSettings = SomneoApiClient.GetAlarmSettings(position);

                if (alarmSettings == null)
                {
                    Console.WriteLine($"No alarm set for position #{position}.");
                    return;
                }

                string daysState = null;
                if (alarmSettings.RepeatDays.Count > 0)
                    daysState = $"{Environment.NewLine}  Days: {string.Join(",", alarmSettings.RepeatDays.Select(d => string.Concat(d.ToString().Take(3))))}";

                string channelOrPresetState = null;
                switch (alarmSettings.Device)
                {
                    case SoundDeviceType.FMRadio:
                        int? fmRadioPreset = alarmSettings.GetFMRadioPreset();
                        if (fmRadioPreset.HasValue)
                            channelOrPresetState = $"{Environment.NewLine}  FM-radio preset: {fmRadioPreset.Value}";
                        break;
                    case SoundDeviceType.WakeUpSound:
                        WakeUpSound? wakeUpSound = alarmSettings.GetWakeUpSound();
                        if (wakeUpSound.HasValue)
                            channelOrPresetState = $"{Environment.NewLine}  Wake-up sound: {EnumHelper.GetDescription(wakeUpSound.Value)}";
                        break;
                }

                string sunriseState = null;
                if (alarmSettings.SunriseType != SunriseType.NoLight)
                    sunriseState = $" (intensity: {alarmSettings.SunriseIntensity}/25, duration: {alarmSettings.SunriseDuration}/40 minutes)";

                string soundVolumeState = null;
                if (alarmSettings.Device != SoundDeviceType.None)
                    soundVolumeState = $" (volume: {alarmSettings.Volume}/25)";

                Console.WriteLine(
$@"Alarm #{alarmSettings.Position} settings:
  Enabled: {(alarmSettings.Enabled ? "Yes" : "No")}
  Time: {alarmSettings.Hour:00}:{alarmSettings.Minute:00}{daysState}
  PowerWake: {(alarmSettings.PowerWakeEnabled ? $"{alarmSettings.PowerWakeHour.Value:00}:{alarmSettings.PowerWakeMinute.Value:00}" : "off")}
  Sunrise: {EnumHelper.GetDescription(alarmSettings.SunriseType)}{sunriseState}
  Sound device: {EnumHelper.GetDescription(alarmSettings.Device)}{soundVolumeState}{channelOrPresetState}");

                return;
            }

            Console.WriteLine("Specify a position between 1 and 16.");
        }

        private void ToggleAlarm(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
                string[] argsArray = args.Split(new[] { ' ' }, 2);

                if (argsArray.Length == 2
                    && int.TryParse(argsArray[0], out int position)
                    && position >= 1 && position <= 16)
                {
                    switch (argsArray[1].ToLower())
                    {
                        case "on":
                            SomneoApiClient.ToggleAlarm(position, true);
                            Console.WriteLine($"Alarm #{position} enabled.");
                            return;

                        case "off":
                            SomneoApiClient.ToggleAlarm(position, true);
                            Console.WriteLine($"Alarm #{position} disabled.");
                            return;
                    }
                }
            }

            Console.WriteLine("Specify a position between 1 and 16, followed by \"on\" or \"off\".");
        }

        private void RemoveAlarm(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int position) && position >= 1 && position <= 16)
            {
                SomneoApiClient.RemoveAlarm(position);
                Console.WriteLine($"Alarm #{position} removed.");
                return;
            }

            Console.WriteLine("Specify a position between 1 and 16.");
        }

        private void SetSnoozeTime(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int minutes) && minutes >= 1 && minutes <= 20)
            {
                SomneoApiClient.SetSnoozeTime(minutes);
                Console.WriteLine($"Snooze set to {minutes} minute(s).");
                return;
            }

            Console.WriteLine("Specify snooze minutes between 1 and 20.");
        }
    }
}
