using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class LightCommandHandler : CommandHandlerBase
{
    public LightCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("light", "Show the light state.", ShowLightState);
        commandRegistry.RegisterCommand("toggle-light", "[on/off]", "Toggle the light.", ToggleLight);
        commandRegistry.RegisterCommand("set-light-level", "[1-25]", "Set the light level.", SetLightLevel);
        commandRegistry.RegisterCommand("toggle-night-light", "[on/off]", "Toggle the night light.", ToggleNightLight);
        commandRegistry.RegisterCommand("toggle-sunrise-preview", "[on/off]", "Toggle the Sunrise Preview mode.", ToggleSunrisePreview);
    }

    private void ShowLightState(string? args)
    {
        LightState lightState = SomneoApiClient.GetLightState();

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

    private void ToggleLight(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                SomneoApiClient.ToggleLight(true);
                Console.WriteLine("Light enabled.");
                break;

            case "off":
                SomneoApiClient.ToggleLight(false);
                Console.WriteLine("Light disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }

    private void SetLightLevel(string? args)
    {
        if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int level) && level >= 1 && level <= 25)
        {
            SomneoApiClient.SetLightLevel(level);
            Console.WriteLine($"Light level set to {level}/25.");
            return;
        }

        Console.WriteLine("Specify a light level between 1 and 25.");
    }

    private void ToggleNightLight(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                SomneoApiClient.ToggleNightLight(true);
                Console.WriteLine("Night light enabled.");
                break;

            case "off":
                SomneoApiClient.ToggleNightLight(false);
                Console.WriteLine("Night light disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }

    private void ToggleSunrisePreview(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                SomneoApiClient.ToggleSunrisePreview(true);
                Console.WriteLine("Sunrise preview enabled.");
                break;

            case "off":
                SomneoApiClient.ToggleSunrisePreview(true);
                Console.WriteLine("Sunrise preview disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }
}
