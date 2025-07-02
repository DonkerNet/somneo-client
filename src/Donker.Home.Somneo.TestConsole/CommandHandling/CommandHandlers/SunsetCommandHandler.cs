using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class SunsetCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("sunset-settings", "Show the sunset settings.", ShowSunsetSettingsAsync);
        commandRegistry.RegisterCommand("toggle-sunset", "[on/off]", "Toggle the sunset.", ToggleSunsetAsync);
        commandRegistry.RegisterCommand(
            "set-fm-radio-sunset",
            "[1-3] [1-25] [5-60] [1-5] [1-25]",
            "Configures the sunset to use the specified colors, intensity, duration, FM radio preset and volume.",
            args => SetSunsetSettingsAsync(args, SoundDeviceType.FMRadio));
        commandRegistry.RegisterCommand(
            "set-sunset-sound-sunset",
            "[1-3] [1-25] [5-60] [1-4] [1-25]",
            "Configures the sunset to use the specified colors, intensity, duration, sunset sound and volume.",
            args => SetSunsetSettingsAsync(args, SoundDeviceType.Sunset));
        commandRegistry.RegisterCommand(
            "set-silent-sunset",
            "[1-3] [1-25] [5-60]",
            "Configures the sunset to use the specified colors, intensity and duration.",
            args => SetSunsetSettingsAsync(args, null));
    }

    private async Task ShowSunsetSettingsAsync(string? args)
    {
        var sunsetSettings = await SomneoApiClient.GetSunsetSettingsAsync();

        string soundDevice = sunsetSettings.SoundDevice.HasValue ? EnumHelper.GetDescription(sunsetSettings.SoundDevice.Value)! : "None";

        string? channelOrPresetState = null;
        switch (sunsetSettings.SoundDevice)
        {
            case SoundDeviceType.FMRadio:
                if (sunsetSettings.FMRadioPreset.HasValue)
                    channelOrPresetState = $"{Environment.NewLine}  FM-radio preset: {sunsetSettings.FMRadioPreset.Value}";
                break;
            case SoundDeviceType.Sunset:
                if (sunsetSettings.SunsetSound.HasValue)
                    channelOrPresetState = $"{Environment.NewLine}  Sunset sound: {EnumHelper.GetDescription(sunsetSettings.SunsetSound.Value)}";
                break;
        }

        string? soundVolumeState = null;
        if (sunsetSettings.SoundDevice.HasValue)
            soundVolumeState = $" (volume: {sunsetSettings.Volume}/25)";

        Console.WriteLine(
$@"Sunset settings:
  Enabled: {(sunsetSettings.Enabled ? "Yes" : "No")}
  Colors: {EnumHelper.GetDescription(sunsetSettings.SunsetColors)}
  Intensity: {sunsetSettings.SunsetIntensity}/25
  Duration: {sunsetSettings.SunsetDuration}/40 minutes
  Sound device: {soundDevice}{soundVolumeState}{channelOrPresetState}");
    }

    private async Task ToggleSunsetAsync(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                await SomneoApiClient.ToggleSunsetAsync(true);
                Console.WriteLine("Sunset enabled.");
                break;

            case "off":
                await SomneoApiClient.ToggleSunsetAsync(false);
                Console.WriteLine("Sunset disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }

    private async Task SetSunsetSettingsAsync(string? args, SoundDeviceType? soundDevice)
    {
        if (string.IsNullOrEmpty(args))
        {
            Console.WriteLine("Specify the parameters to configure the sunset with.");
            return;
        }

        string[] argsArray = args.Split(' ', 11);

        if (argsArray.Length < 3)
        {
            Console.WriteLine("Insufficient number of parameters specified to configure the sunset with.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[0]) || !int.TryParse(argsArray[0], out int sunsetColorNumber) || !EnumHelper.TryCast(sunsetColorNumber - 1, out ColorScheme sunsetColors))
        {
            Console.WriteLine("Specify a valid sunset color scheme between 1 and 3.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[1]) || !int.TryParse(argsArray[1], out int sunsetIntensity) || sunsetIntensity < 1 || sunsetIntensity > 25)
        {
            Console.WriteLine("Specify a sunset intensity between 1 and 25.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[2]) || !int.TryParse(argsArray[2], out int sunsetDuration) || sunsetDuration < 5 || sunsetDuration > 60 || sunsetDuration % 5 != 0)
        {
            Console.WriteLine("Specify a sunset duration between 5 and 60, with 5 minutes in between.");
            return;
        }

        int fmRadioPreset = 0;
        SunsetSound sunsetSound = default;

        switch (soundDevice)
        {
            case SoundDeviceType.FMRadio:
                if (string.IsNullOrEmpty(argsArray[3]) || !int.TryParse(argsArray[3], out fmRadioPreset) || fmRadioPreset < 1 || fmRadioPreset > 5)
                {
                    Console.WriteLine("Specify an FM radio present between 1 and 5.");
                    return;
                }
                break;

            case SoundDeviceType.Sunset:
                if (string.IsNullOrEmpty(argsArray[3]) || !int.TryParse(argsArray[3], out int sunsetSoundNumber) || !EnumHelper.TryCast(sunsetSoundNumber - 1, out sunsetSound))
                {
                    Console.WriteLine("Specify a sunset sound between 1 and 4.");
                    return;
                }
                break;
        }

        int volume = 0;
        if (soundDevice.HasValue && (string.IsNullOrEmpty(argsArray[4]) || !int.TryParse(argsArray[4], out volume) || volume < 1 || volume > 25))
        {
            Console.WriteLine("Specify a volume between 1 and 25.");
            return;
        }

        switch (soundDevice)
        {
            case SoundDeviceType.FMRadio:
                await SomneoApiClient.SetSunsetSettingsWithFMRadioAsync(
                    sunsetColors,
                    sunsetIntensity,
                    sunsetDuration,
                    fmRadioPreset,
                    volume);
                break;

            case SoundDeviceType.Sunset:
                await SomneoApiClient.SetSunsetSettingsWithSunsetSoundAsync(
                    sunsetColors,
                    sunsetIntensity,
                    sunsetDuration,
                    sunsetSound,
                    volume);
                break;

            case null:
                await SomneoApiClient.SetSunsetSettingsWithoutSoundAsync(
                    sunsetColors,
                    sunsetIntensity,
                    sunsetDuration);
                break;
        }

        string? soundDeviceState = null;
        switch (soundDevice)
        {
            case SoundDeviceType.FMRadio:
                soundDeviceState = $" (volume: {volume}/25){Environment.NewLine}  FM-radio preset: {fmRadioPreset}";
                break;
            case SoundDeviceType.Sunset:
                soundDeviceState = $" (volume: {volume}/25){Environment.NewLine}  Sunset sound: {EnumHelper.GetDescription((SunsetSound)sunsetSound)}";
                break;
        }

        Console.WriteLine(
$@"Updated sunset with the settings:
  Sunset: {EnumHelper.GetDescription(sunsetColors)} (intensity: {sunsetIntensity}/25, duration: {sunsetDuration}/60 minutes)
  Sound device: {(soundDevice.HasValue ? EnumHelper.GetDescription(soundDevice.Value) : "None")}{soundDeviceState}");
    }
}
