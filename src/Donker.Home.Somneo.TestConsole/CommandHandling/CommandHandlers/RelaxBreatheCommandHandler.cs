using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class RelaxBreatheCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("relax-breathe-settings", "Show the RelaxBreathe settings.", ShowRelaxBreatheSettingsAsync);
        commandRegistry.RegisterCommand("toggle-relax-breathe", "[on/off]", "Toggle RelaxBreathe on or off.", ToggleRelaxBreatheAsync);
        commandRegistry.RegisterCommand(
            "set-relax-breathe-with-sound",
            "[5,10,15] <option> [1-25]",
            "Configures RelaxBreathe with sound to use the specified duration, breaths per minute option and volume.",
            SetRelaxBreatheWithSoundAsync);
        commandRegistry.RegisterCommand(
            "set-relax-breathe-with-light",
            "[5,10,15] <option> [1-25]",
            "Configures RelaxBreathe with light to use the specified duration, breaths per minute option and intensity.",
            SetRelaxBreatheWithLightAsync);
    }

    private async Task ShowRelaxBreatheSettingsAsync(string? args)
    {
        var relaxBreatheSettings = await SomneoApiClient.GetRelaxBreatheSettingsAsync();

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

    private async Task ToggleRelaxBreatheAsync(string? args)
    {
        switch (args?.ToLower())
        {
            case "on":
                await SomneoApiClient.ToggleRelaxBreatheAsync(true);
                Console.WriteLine("RelaxBreathe enabled.");
                break;

            case "off":
                await SomneoApiClient.ToggleRelaxBreatheAsync(false);
                Console.WriteLine("RelaxBreathe disabled.");
                break;

            default:
                Console.WriteLine("Specify \"on\" or \"off\".");
                break;
        }
    }

    private async Task SetRelaxBreatheWithSoundAsync(string? args)
    {
        if (string.IsNullOrEmpty(args))
        {
            Console.WriteLine("Specify the parameters to configure RelaxBreathe with.");
            return;
        }

        string[] argsArray = args.Split(' ', 11);

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

        await SomneoApiClient.SetRelaxBreatheSettingsWithSoundAsync(
            duration,
            bpmOption,
            volume);

        Console.WriteLine(
$@"Updated RelaxBreathe to use sound with the settings:
  Duration: {duration}/15 minutes
  Breaths per minute option: {bpmOption}
  Volume: {volume}/25");
    }

    private async Task SetRelaxBreatheWithLightAsync(string? args)
    {
        if (string.IsNullOrEmpty(args))
        {
            Console.WriteLine("Specify the parameters to configure RelaxBreathe with.");
            return;
        }

        string[] argsArray = args.Split(' ', 11);

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

        await SomneoApiClient.SetRelaxBreatheSettingsWithLightAsync(
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
