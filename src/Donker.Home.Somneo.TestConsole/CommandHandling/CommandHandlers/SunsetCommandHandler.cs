using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Models;

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

        string? channelOrPresetState = null;
        switch (sunsetSettings.Device)
        {
            case SoundDeviceType.FMRadio:
                int? fmRadioPreset = sunsetSettings.GetFMRadioPreset();
                if (fmRadioPreset.HasValue)
                    channelOrPresetState = $"{Environment.NewLine}  FM-radio preset: {fmRadioPreset.Value}";
                break;
            case SoundDeviceType.Sunset:
                SunsetSound? sunsetSound = sunsetSettings.GetSunsetSound();
                if (sunsetSound.HasValue)
                    channelOrPresetState = $"{Environment.NewLine}  Sunset sound: {EnumHelper.GetDescription(sunsetSound.Value)}";
                break;
        }

        string? soundVolumeState = null;
        if (sunsetSettings.Device != SoundDeviceType.None)
            soundVolumeState = $" (volume: {sunsetSettings.Volume}/25)";

        Console.WriteLine(
$@"Sunset settings:
  Enabled: {(sunsetSettings.Enabled ? "Yes" : "No")}
  Colors: {EnumHelper.GetDescription(sunsetSettings.SunsetColors)}
  Intensity: {sunsetSettings.SunsetIntensity}/25
  Duration: {sunsetSettings.SunsetDuration}/40 minutes
  Sound device: {EnumHelper.GetDescription(sunsetSettings.Device)}{soundVolumeState}{channelOrPresetState}");
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
