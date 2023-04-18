using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

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
        commandRegistry.RegisterCommand("toggle-relax-breathe", "[on/off]", "Toggle RelaxBreathe on or off.", ToggleRelaxBreathe);
        commandRegistry.RegisterCommand(
            "set-relax-breathe-with-sound",
            "[5,10,15] <option> [1-25]",
            "Configures RelaxBreathe with sound to use the specified duration, breaths per minute option and volume.",
            SetRelaxBreatheWithSound);
        commandRegistry.RegisterCommand(
            "set-relax-breathe-with-light",
            "[5,10,15] <option> [1-25]",
            "Configures RelaxBreathe with light to use the specified duration, breaths per minute option and intensity.",
            SetRelaxBreatheWithLight);
    }

    private void ShowRelaxBreatheSettings(string? args)
    {
        RelaxBreatheSettings relaxBreatheSettings = SomneoApiClient.GetRelaxBreatheSettings();

        string intensityOrVolume = relaxBreatheSettings.IsLight
            ? $"Intensity: {relaxBreatheSettings.LightIntensity}/25"
            : $"Volume: {relaxBreatheSettings.SoundVolume}/25";

        var availableBpms = relaxBreatheSettings.AvailableBreathsPerMinute.Select((bpm, index) => $"  {index} => {bpm} bpm");

        Console.WriteLine(
$@"RelaxBreathe settings:
  Enabled: {(relaxBreatheSettings.Enabled ? "Yes" : "No")}
  Breaths per minute: {relaxBreatheSettings.BreathsPerMinute}
  Duration: {relaxBreatheSettings.Duration}/15 minutes
  Type: {(relaxBreatheSettings.IsLight ? "Light" : "Sound")}
  {intensityOrVolume}

Breaths per minute options:
{string.Join(Environment.NewLine, availableBpms)}");
    }

    private void ToggleRelaxBreathe(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                SomneoApiClient.ToggleRelaxBreathe(true);
                Console.WriteLine("RelaxBreathe enabled.");
                break;

            case "off":
                SomneoApiClient.ToggleRelaxBreathe(false);
                Console.WriteLine("RelaxBreathe disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }

    private void SetRelaxBreatheWithSound(string? args)
    {
        if (string.IsNullOrEmpty(args))
        {
            Console.WriteLine("Specify the parameters to configure RelaxBreathe with.");
            return;
        }

        string[] argsArray = args.Split(new[] { ' ' }, 11);

        if (argsArray.Length < 3)
        {
            Console.WriteLine("Insufficient number of parameters specified to configure RelaxBreathe with.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[0]) || !int.TryParse(argsArray[0], out int duration) || duration < 5 || duration > 15 || duration % 5 != 0)
        {
            Console.WriteLine("Specify a duration of 5, 10 or 15 minutes.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[1]) || !int.TryParse(argsArray[1], out int bpmOption))
        {
            Console.WriteLine("Specify an option (index) for the amount of breaths per minute.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[2]) || !int.TryParse(argsArray[2], out int volume) || volume < 1 || volume > 25)
        {
            Console.WriteLine("Specify a volume between 1 and 25.");
            return;
        }

        SomneoApiClient.SetRelaxBreatheSettingsWithSound(
            duration,
            bpmOption,
            volume);

        Console.WriteLine(
$@"Updated RelaxBreathe to use sound with the settings:
  Duration: {duration}/15 minutes
  Breaths per minute option: {bpmOption}
  Volume: {volume}/25");
    }

    private void SetRelaxBreatheWithLight(string? args)
    {
        if (string.IsNullOrEmpty(args))
        {
            Console.WriteLine("Specify the parameters to configure RelaxBreathe with.");
            return;
        }

        string[] argsArray = args.Split(new[] { ' ' }, 11);

        if (argsArray.Length < 3)
        {
            Console.WriteLine("Insufficient number of parameters specified to configure RelaxBreathe with.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[0]) || !int.TryParse(argsArray[0], out int duration) || duration < 5 || duration > 15 || duration % 5 != 0)
        {
            Console.WriteLine("Specify a duration of 5, 10 or 15 minutes.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[1]) || !int.TryParse(argsArray[1], out int bpmOption))
        {
            Console.WriteLine("Specify an option (index) for the amount of breaths per minute.");
            return;
        }

        if (string.IsNullOrEmpty(argsArray[2]) || !int.TryParse(argsArray[2], out int intensity) || intensity < 1 || intensity > 25)
        {
            Console.WriteLine("Specify an intensity between 1 and 25.");
            return;
        }

        SomneoApiClient.SetRelaxBreatheSettingsWithLight(
            duration,
            bpmOption,
            intensity);

        Console.WriteLine(
$@"Updated RelaxBreathe to use light with the settings:
  Duration: {duration}/15 minutes
  Breaths per minute option: {bpmOption}
  Intensity: {intensity}/25");
    }
}
