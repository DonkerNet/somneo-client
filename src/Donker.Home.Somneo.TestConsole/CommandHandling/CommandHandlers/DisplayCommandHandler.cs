using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class DisplayCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("display", "Show the display state.", ShowDisplayStateAsync);
        commandRegistry.RegisterCommand("toggle-permanent-display", "[on/off]", "Toggle the permanent display.", TogglePermanentDisplayAsync);
        commandRegistry.RegisterCommand("set-display-level", "[1-6]", "Set the display level.", SetDisplayLevelAsync);
    }

    private async Task ShowDisplayStateAsync(string? args)
    {
        var displayState = await SomneoApiClient.GetDisplayStateAsync();

        Console.WriteLine(
$@"Display state:
  Permanent display enabled: {(displayState.Permanent ? "Yes" : "No")}
  Brightness level: {displayState.DisplayLevel}/6");
    }

    private async Task TogglePermanentDisplayAsync(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                await SomneoApiClient.TogglePermanentDisplayAsync(true);
                Console.WriteLine("Permanent display enabled.");
                break;

            case "off":
                await SomneoApiClient.TogglePermanentDisplayAsync(false);
                Console.WriteLine("Permanent display disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }

    private async Task SetDisplayLevelAsync(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int level) && level >= 1 && level <= 6)
        {
            await SomneoApiClient.SetDisplayLevelAsync(level);
            Console.WriteLine($"Display brightness level set to {level}/6.");
            return;
        }

        Console.WriteLine("Specify a light level between 1 and 6.");
    }
}
