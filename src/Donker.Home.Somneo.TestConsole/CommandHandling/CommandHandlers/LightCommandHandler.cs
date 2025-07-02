using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class LightCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("light", "Show the light state.", ShowLightStateAsync);
        commandRegistry.RegisterCommand("toggle-light", "[on/off]", "Toggle the light.", ToggleLightAsync);
        commandRegistry.RegisterCommand("set-light-level", "[1-25]", "Set the light level.", SetLightLevelAsync);
        commandRegistry.RegisterCommand("toggle-night-light", "[on/off]", "Toggle the night light.", ToggleNightLightAsync);
    }

    private async Task ShowLightStateAsync(string? args)
    {
        var lightState = await SomneoApiClient.GetLightStateAsync();

        Console.WriteLine(
$@"Light state:
  Normal light enabled: {(lightState.LightEnabled ? "Yes" : "No")}
  Light level: {lightState.LightLevel}/25
  Sunrise/sunset enabled: {(lightState.SunriseOrSunsetEnabled ? "Yes" : "No")}
  Night light enabled: {(lightState.NightLightEnabled ? "Yes" : "No")}");
    }

    private async Task ToggleLightAsync(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                await SomneoApiClient.ToggleLightAsync(true);
                Console.WriteLine("Light enabled.");
                break;

            case "off":
                await SomneoApiClient.ToggleLightAsync(false);
                Console.WriteLine("Light disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }

    private async Task SetLightLevelAsync(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int level) && level >= 1 && level <= 25)
        {
            await SomneoApiClient.SetLightLevelAsync(level);
            Console.WriteLine($"Light level set to {level}/25.");
            return;
        }

        Console.WriteLine("Specify a light level between 1 and 25.");
    }

    private async Task ToggleNightLightAsync(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                await SomneoApiClient.ToggleNightLightAsync(true);
                Console.WriteLine("Night light enabled.");
                break;

            case "off":
                await SomneoApiClient.ToggleNightLightAsync(false);
                Console.WriteLine("Night light disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }
}
