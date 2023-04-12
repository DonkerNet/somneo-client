using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class RelaxBreatheCommandHandler : CommandHandlerBase
{
    public RelaxBreatheCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("relax-breathe-settings", "Show the RelaxBreathe settings.", ShowRelaxBreatheSettings);
    }

    private void ShowRelaxBreatheSettings(string? args)
    {
        RelaxBreatheSettings relaxBreatheSettings = SomneoApiClient.GetRelaxBreatheSettings();

        string intensityOrVolume = relaxBreatheSettings.IsLight
            ? $"Intensity: {relaxBreatheSettings.LightIntensity}/25"
            : $"Volume: {relaxBreatheSettings.SoundVolume}/25";

        Console.WriteLine(
$@"RelaxBreathe settings:
  Enabled: {(relaxBreatheSettings.Enabled ? "Yes" : "No")}
  Breaths per minute: {relaxBreatheSettings.BreathsPerMinute}/10
  Duration: {relaxBreatheSettings.Duration}/15 minutes
  Type: {(relaxBreatheSettings.IsLight ? "Light" : "Sound")}
  {intensityOrVolume}");
    }
}
