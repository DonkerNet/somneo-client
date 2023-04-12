using System.Text;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

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
        commandRegistry.RegisterCommand(
            "set-fm-radio-alarm",
            "[1-16] [0-23] [0-59] [0-59,_] [sunday-saturday|...,_] [1-3,_] [1-25,_] [5-40,_] [1-5] [1-25]",
            "Sets an alarm for the specified position, hour, minute, PowerWake minutes, repeat days, sunrise colors, sunrise intensity, sunrise duration, FM radio preset and volume.",
            args => SetAlarm(args, SoundDeviceType.FMRadio));
        commandRegistry.RegisterCommand(
            "set-wake-up-sound-alarm",
            "[1-16] [0-23] [0-59] [0-59,_] [sunday-saturday|...,_] [1-3,_] [1-25,_] [5-40,_] [1-8] [1-25]",
            "Sets an alarm for the specified position, hour, minute, PowerWake minutes, repeat days, sunrise colors, sunrise intensity, sunrise duration, wake-up sound and volume.",
            args => SetAlarm(args, SoundDeviceType.WakeUpSound));
        commandRegistry.RegisterCommand(
            "set-silent-alarm",
            "[1-16] [0-23] [0-59] [0-59,_] [sunday-saturday|...,_] [1-3] [1-25] [5-40]",
            "Sets an alarm with only a sunrise and no sound for the specified position, hour, minute, PowerWake minutes, repeat days, sunrise colors, sunrise intensity and sunrise duration.",
            args => SetAlarm(args, null));
        commandRegistry.RegisterCommand("remove-alarm", "[1-16]", "Remove an alarm.", RemoveAlarm);
        commandRegistry.RegisterCommand("set-snooze-time", "[1-20]", "Sets the snooze time in minutes.", SetSnoozeTime);
    }

    private void ShowAlarms(string? args)
    {
        IReadOnlyList<Alarm> alarms = SomneoApiClient.GetAlarms();

        if (alarms.Count == 0)
        {
            Console.WriteLine("0/16 alarms set.");
            return;
        }

        var consoleMessageBuilder = new StringBuilder();

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
alarm.PowerWakeEnabled ? $"{alarm.PowerWakeHour!:00}:{alarm.PowerWakeMinute!:00}" : "off");
        }

        Console.WriteLine(consoleMessageBuilder);
    }

    private void ShowAlarmSettings(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int position) && position >= 1 && position <= 16)
        {
            AlarmSettings? alarmSettings = SomneoApiClient.GetAlarmSettings(position);

            if (alarmSettings == null)
            {
                Console.WriteLine($"No alarm set for position #{position}.");
                return;
            }

            string? daysState = null;
            if (alarmSettings.RepeatDays.Count > 0)
                daysState = $"{Environment.NewLine}  Days: {string.Join(",", alarmSettings.RepeatDays.Select(d => string.Concat(d.ToString().Take(3))))}";

            string soundDevice = alarmSettings.Device.HasValue ? EnumHelper.GetDescription(alarmSettings.Device.Value)! : "None";

            string? channelOrPresetState = null;
            switch (alarmSettings.Device)
            {
                case SoundDeviceType.FMRadio:
                    if (alarmSettings.FMRadioPreset.HasValue)
                        channelOrPresetState = $"{Environment.NewLine}  FM-radio preset: {alarmSettings.FMRadioPreset.Value}";
                    break;
                case SoundDeviceType.WakeUpSound:
                    if (alarmSettings.WakeUpSound.HasValue)
                        channelOrPresetState = $"{Environment.NewLine}  Wake-up sound: {EnumHelper.GetDescription(alarmSettings.WakeUpSound.Value)}";
                    break;
            }

            string? sunriseState = null;
            if (alarmSettings.SunriseColors.HasValue)
                sunriseState = $" (intensity: {alarmSettings.SunriseIntensity}/25, duration: {alarmSettings.SunriseDuration}/40 minutes)";

            string? soundVolumeState = null;
            if (alarmSettings.Device.HasValue)
                soundVolumeState = $" (volume: {alarmSettings.Volume}/25)";

            Console.WriteLine(
$@"Alarm #{alarmSettings.Position} settings:
  Enabled: {(alarmSettings.Enabled ? "Yes" : "No")}
  Time: {alarmSettings.Hour:00}:{alarmSettings.Minute:00}{daysState}
  PowerWake: {(alarmSettings.PowerWakeEnabled ? $"{alarmSettings.PowerWakeHour!:00}:{alarmSettings.PowerWakeMinute!:00}" : "off")}
  Sunrise: {(alarmSettings.SunriseColors.HasValue ? EnumHelper.GetDescription(alarmSettings.SunriseColors.Value) : "none")}{sunriseState}
  Sound device: {soundDevice}{soundVolumeState}{channelOrPresetState}");

            return;
        }

        Console.WriteLine("Specify a position between 1 and 16.");
    }

    private void SetAlarm(string? args, SoundDeviceType? soundDevice)
    {
        if (string.IsNullOrEmpty(args))
        {
            Console.WriteLine("Specify the parameters to configure the alarm with.");
            return;
        }

        string[] argsArray = args.Split(new[] { ' ' }, 11);

        if (argsArray.Length < 8)
        {
            Console.WriteLine("Insufficient number of parameters specified to configure the alarm with.");
            return;
        }

        if (!int.TryParse(argsArray[0], out int position) || position < 1 || position > 16)
        {
            Console.WriteLine("Specify a position between 1 and 16.");
            return;
        }

        if (!int.TryParse(argsArray[1], out int hour) || hour < 0 || hour > 23)
        {
            Console.WriteLine("Specify an hour between 0 and 23.");
            return;
        }

        if (!int.TryParse(argsArray[2], out int minute) || minute < 0 || minute > 59)
        {
            Console.WriteLine("Specify a minute between 0 and 59.");
            return;
        }

        int? definitivePowerWakeMinutes = null;
        if (argsArray[3] != "_")
        {
            if (!int.TryParse(argsArray[3], out int powerWakeMinutes) || powerWakeMinutes < 0 || powerWakeMinutes > 59)
            {
                Console.WriteLine("Specify the PowerWake minutes between 0 and 59, or an underscore (_) to leave empty.");
                return;
            }

            definitivePowerWakeMinutes = powerWakeMinutes;
        }

        var repeatDays = new HashSet<DayOfWeek>();
        if (argsArray[4] != "_")
        {
            foreach (string repeatDayString in argsArray[4].Split('|'))
            {
                if (!Enum.TryParse(repeatDayString, true, out DayOfWeek repeatDay) || !Enum.IsDefined(repeatDay))
                {
                    Console.WriteLine("Specify one or more repeat days that are valid days of the week, or an underscore (_) to leave empty.");
                    return;
                }

                repeatDays.Add(repeatDay);
            }
        }

        ColorScheme? definitiveSunriseColors = null;
        if (argsArray[5] != "_")
        {
            if (!int.TryParse(argsArray[5], out int sunriseColorsNumber) || !EnumHelper.TryCast(sunriseColorsNumber - 1, out ColorScheme sunriseColors))
            {
                Console.WriteLine("Specify a valid sunrise color scheme between 1 and 3.");
                return;
            }

            definitiveSunriseColors = sunriseColors;
        }

        if (!soundDevice.HasValue && !definitiveSunriseColors.HasValue)
        {
            Console.WriteLine("Specify a sunrise color scheme.");
            return;
        }

        int? definitiveSunriseIntensity = null;
        if (argsArray[6] != "_")
        {
            if (!int.TryParse(argsArray[6], out int sunriseIntensity) || sunriseIntensity < 1 || sunriseIntensity > 25)
            {
                Console.WriteLine("Specify a sunrise intensity between 1 and 25, or an underscore (_) to leave empty.");
                return;
            }

            definitiveSunriseIntensity = sunriseIntensity;
        }

        if (definitiveSunriseColors.HasValue && !definitiveSunriseIntensity.HasValue)
        {
            Console.WriteLine("Specify a sunrise intensity when the sunrise colors are set.");
            return;
        }

        int? definitiveSunriseDuration = null;
        if (argsArray[7] != "_")
        {
            if (!int.TryParse(argsArray[7], out int sunriseDuration) || sunriseDuration < 5 || sunriseDuration > 40 || sunriseDuration % 5 != 0)
            {
                Console.WriteLine("Specify a sunrise duration between 5 and 40, with 5 minutes in between, or an underscore (_) to leave empty.");
                return;
            }

            definitiveSunriseDuration = sunriseDuration;
        }

        if (definitiveSunriseColors.HasValue && !definitiveSunriseDuration.HasValue)
        {
            Console.WriteLine("Specify a sunrise duration when the sunrise colors are set.");
            return;
        }

        int fmRadioPreset = 0;
        WakeUpSound wakeUpSound = default;

        switch (soundDevice)
        {
            case SoundDeviceType.FMRadio:
                if (string.IsNullOrEmpty(argsArray[8]) || !int.TryParse(argsArray[8], out fmRadioPreset) || fmRadioPreset < 1 || fmRadioPreset > 5)
                {
                    Console.WriteLine("Specify an FM radio present between 1 and 5.");
                    return;
                }
                break;

            case SoundDeviceType.WakeUpSound:
                if (string.IsNullOrEmpty(argsArray[8]) || !int.TryParse(argsArray[8], out int wakeUpSoundNumber) || !EnumHelper.TryCast(wakeUpSoundNumber - 1, out wakeUpSound))
                {
                    Console.WriteLine("Specify a wake-up sound between 1 and 8.");
                    return;
                }
                break;
        }

        int volume = 0;
        if (soundDevice.HasValue && (string.IsNullOrEmpty(argsArray[9]) || !int.TryParse(argsArray[9], out volume) || volume < 1 || volume > 25))
        {
            Console.WriteLine("Specify a volume between 1 and 25.");
            return;
        }

        switch (soundDevice)
        {
            case SoundDeviceType.FMRadio:
                SomneoApiClient.SetAlarmWithFMRadio(
                    position,
                    hour,
                    minute,
                    definitivePowerWakeMinutes,
                    repeatDays,
                    definitiveSunriseColors,
                    definitiveSunriseIntensity,
                    definitiveSunriseDuration,
                    fmRadioPreset,
                    volume);
                break;

            case SoundDeviceType.WakeUpSound:
                SomneoApiClient.SetAlarmWithWakeUpSound(
                    position,
                    hour,
                    minute,
                    definitivePowerWakeMinutes,
                    repeatDays,
                    definitiveSunriseColors,
                    definitiveSunriseIntensity,
                    definitiveSunriseDuration,
                    (WakeUpSound)wakeUpSound,
                    volume);
                break;

            case null:
                SomneoApiClient.SetAlarmWithoutSound(
                    position,
                    hour,
                    minute,
                    definitivePowerWakeMinutes,
                    repeatDays,
                    definitiveSunriseColors.GetValueOrDefault(),
                    definitiveSunriseIntensity.GetValueOrDefault(),
                    definitiveSunriseDuration.GetValueOrDefault());
                break;
        }

        string? daysState = null;
        if (repeatDays.Count > 0)
            daysState = $"{Environment.NewLine}  Days: {string.Join(",", repeatDays.Select(d => string.Concat(d.ToString().Take(3))))}";

        string powerWakeState = definitivePowerWakeMinutes.HasValue
            ? new TimeSpan(hour, minute, 0).Add(TimeSpan.FromMinutes(definitivePowerWakeMinutes.Value)).ToString("hh\\:mm")
            : "off";

        string? sunriseState = null;
        if (definitiveSunriseColors.HasValue)
            sunriseState = $" (intensity: {definitiveSunriseIntensity}/25, duration: {definitiveSunriseDuration}/40 minutes)";

        string? soundDeviceState = null;
        switch (soundDevice)
        {
            case SoundDeviceType.FMRadio:
                soundDeviceState = $" (volume: {volume}/25){Environment.NewLine}  FM-radio preset: {fmRadioPreset}";
                break;
            case SoundDeviceType.WakeUpSound:
                soundDeviceState = $" (volume: {volume}/25){Environment.NewLine}  Wake-up sound: {EnumHelper.GetDescription((WakeUpSound)wakeUpSound)}";
                break;
        }

        Console.WriteLine(
$@"Set alarm #{position} with the settings:
  Time: {hour:00}:{minute:00}{daysState}
  PowerWake: {powerWakeState}
  Sunrise: {(definitiveSunriseColors.HasValue ? EnumHelper.GetDescription(definitiveSunriseColors.Value) : "none")}{sunriseState}
  Sound device: {(soundDevice.HasValue ? EnumHelper.GetDescription(soundDevice.Value) : "None")}{soundDeviceState}");
    }

    private void ToggleAlarm(string? args)
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
                        SomneoApiClient.ToggleAlarm(position, false);
                        Console.WriteLine($"Alarm #{position} disabled.");
                        return;
                }
            }
        }

        Console.WriteLine("Specify a position between 1 and 16, followed by \"on\" or \"off\".");
    }

    private void RemoveAlarm(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int position) && position >= 1 && position <= 16)
        {
            SomneoApiClient.RemoveAlarm(position);
            Console.WriteLine($"Alarm #{position} removed.");
            return;
        }

        Console.WriteLine("Specify a position between 1 and 16.");
    }

    private void SetSnoozeTime(string? args)
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
