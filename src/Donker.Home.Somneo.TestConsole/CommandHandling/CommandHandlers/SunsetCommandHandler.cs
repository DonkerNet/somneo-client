using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class SunsetCommandHandler : CommandHandlerBase
{
    public SunsetCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("sunset-settings", "Show the sunset settings.", ShowSunsetSettings);
        commandRegistry.RegisterCommand("toggle-sunset", "[on/off]", "Toggle the sunset.", ToggleSunset);
    }

    private void ShowSunsetSettings(string? args)
    {
        SunsetSettings sunsetSettings = SomneoApiClient.GetSunsetSettings();

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

    private void ToggleSunset(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                SomneoApiClient.ToggleSunset(true);
                Console.WriteLine("Sunset enabled.");
                break;

            case "off":
                SomneoApiClient.ToggleSunset(false);
                Console.WriteLine("Sunset disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }
}
