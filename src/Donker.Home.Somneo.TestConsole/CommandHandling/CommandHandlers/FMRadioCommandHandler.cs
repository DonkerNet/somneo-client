using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class FMRadioCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("fm-radio-presets", "Show the FM-radio presets.", ShowFMRadioPresetsAsync);
        commandRegistry.RegisterCommand("get-fm-radio-preset", "[1-5]", "Gets the frequency of an FM-radio preset.", GetFMRadioPresetAsync);
        commandRegistry.RegisterCommand("fm-radio", "Show the FM-radio state.", ShowFMRadioStateAsync);
        commandRegistry.RegisterCommand("enable-fm-radio", "Enable the FM-radion.", EnableFMRadioAsync);
        commandRegistry.RegisterCommand("enable-fm-radio-preset", "[1-5]", "Enable an FM-radio preset.", EnableFMRadioPresetAsync);
        commandRegistry.RegisterCommand("seek-fm-radio-station", "[up/down]", "Seek a next FM-radio station.", SeekFMRadioStationAsync);
    }

    private async Task ShowFMRadioPresetsAsync(string? args)
    {
        var fmRadioPresets = await SomneoApiClient.GetFMRadioPresetsAsync();

        Console.WriteLine(
$@"FM radio presets:
  1: {fmRadioPresets.Preset1:0.00} FM
  2: {fmRadioPresets.Preset2:0.00} FM
  3: {fmRadioPresets.Preset3:0.00} FM
  4: {fmRadioPresets.Preset4:0.00} FM
  5: {fmRadioPresets.Preset5:0.00} FM");
    }

    private async Task GetFMRadioPresetAsync(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int preset) && preset >= 1 && preset <= 5)
        {
            float frequency = await SomneoApiClient.GetFMRadioPresetAsync(preset);
            Console.WriteLine($"Preset {preset} is currently set to {frequency:0.00} FM.");
            return;
        }

        Console.WriteLine("Specify a position between 1 and 5.");
    }

    private async Task ShowFMRadioStateAsync(string? args)
    {
        var fmRadioState = await SomneoApiClient.GetFMRadioStateAsync();

        Console.WriteLine(
$@"FM radio state:
  Frequency: {fmRadioState.Frequency:0.00} FM
  Preset: {fmRadioState.Preset}/5");
    }

    private async Task EnableFMRadioAsync(string? args)
    {
        await SomneoApiClient.EnableFMRadioAsync();
        Console.WriteLine("FM radio enabled for the current preset.");
    }

    private async Task EnableFMRadioPresetAsync(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int preset) && preset >= 1 && preset <= 5)
        {
            await SomneoApiClient.EnableFMRadioPresetAsync(preset);
            Console.WriteLine($"FM radio enabled for preset {preset}/5.");
            return;
        }

        Console.WriteLine("The preset should be between 1 and 5.");
    }

    private async Task SeekFMRadioStationAsync(string? args)
    {
        switch (args?.ToLower())
        {
            case "up":
                await SomneoApiClient.SeekFMRadioStationAsync(RadioSeekDirection.Up);
                Console.WriteLine("Seeking for a new FM radio station in forward direction.");
                break;

            case "down":
                await SomneoApiClient.SeekFMRadioStationAsync(RadioSeekDirection.Down);
                Console.WriteLine("Seeking for a new FM radio station in backward direction.");
                break;

            default:
                Console.WriteLine("Specify \"up\" or \"down\".");
                break;
        }
    }
}
