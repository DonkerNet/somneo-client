using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class FMRadioCommandHandler : CommandHandlerBase
{
    public FMRadioCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("fm-radio-presets", "Show the FM-radio presets.", ShowFMRadioPresets);
        commandRegistry.RegisterCommand("get-fm-radio-preset", "[1-5]", "Gets the frequency of an FM-radio preset.", GetFMRadioPreset);
        commandRegistry.RegisterCommand("fm-radio", "Show the FM-radio state.", ShowFMRadioState);
        commandRegistry.RegisterCommand("enable-fm-radio", "Enable the FM-radion.", EnableFMRadio);
        commandRegistry.RegisterCommand("enable-fm-radio-preset", "[1-5]", "Enable an FM-radio preset.", EnableFMRadioPreset);
        commandRegistry.RegisterCommand("seek-fm-radio-station", "[up/down]", "Seek a next FM-radio station.", SeekFMRadioStation);
    }

    private void ShowFMRadioPresets(string? args)
    {
        FMRadioPresets fmRadioPresets = SomneoApiClient.GetFMRadioPresets();

        Console.WriteLine(
$@"FM radio presets:
  1: {fmRadioPresets.Preset1:0.00} FM
  2: {fmRadioPresets.Preset2:0.00} FM
  3: {fmRadioPresets.Preset3:0.00} FM
  4: {fmRadioPresets.Preset4:0.00} FM
  5: {fmRadioPresets.Preset5:0.00} FM");
    }

    private void GetFMRadioPreset(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int preset) && preset >= 1 && preset <= 5)
        {
            float frequency = SomneoApiClient.GetFMRadioPreset(preset);
            Console.WriteLine($"Preset {preset} is currently set to {frequency:0.00} FM.");
            return;
        }

        Console.WriteLine("Specify a position between 1 and 5.");
    }

    private void ShowFMRadioState(string? args)
    {
        FMRadioState fmRadioState = SomneoApiClient.GetFMRadioState();

        Console.WriteLine(
$@"FM radio state:
  Frequency: {fmRadioState.Frequency:0.00} FM
  Preset: {fmRadioState.Preset}/5");
    }

    private void EnableFMRadio(string? args)
    {
        SomneoApiClient.EnableFMRadio();
        Console.WriteLine("FM radio enabled for the current preset.");
    }

    private void EnableFMRadioPreset(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int preset) && preset >= 1 && preset <= 5)
        {
            SomneoApiClient.EnableFMRadioPreset(preset);
            Console.WriteLine($"FM radio enabled for preset {preset}/5.");
            return;
        }

        Console.WriteLine("The preset should be between 1 and 5.");
    }

    private void SeekFMRadioStation(string? args)
    {
        switch (args?.ToLower())
        {
            case "up":
                SomneoApiClient.SeekFMRadioStation(RadioSeekDirection.Up);
                Console.WriteLine("Seeking for a new FM radio station in forward direction.");
                break;

            case "down":
                SomneoApiClient.SeekFMRadioStation(RadioSeekDirection.Down);
                Console.WriteLine("Seeking for a new FM radio station in backward direction.");
                break;

            default:
                Console.WriteLine("Specify \"up\" or \"down\".");
                break;
        }
    }
}
