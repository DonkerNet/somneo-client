using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class SunriseCommandHandler : CommandHandlerBase
{
    public SunriseCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("enable-sunrise-preview", "[0-2] [1-25]", "Previews a sunrise with the specified intensity.", EnableSunrisePreview);
        commandRegistry.RegisterCommand("disable-sunrise-preview", null, "Disables the sunrise preview.", DisableSunrisePreview);
    }

    private void EnableSunrisePreview(string? args)
    {
        if (!string.IsNullOrEmpty(args))
        {
            string[] argsArray = args.Split(new[] { ' ' }, 2);

            if (argsArray.Length == 2
                && Enum.TryParse(argsArray[0], out ColorScheme colorScheme)
                && Enum.IsDefined(colorScheme)
                && int.TryParse(argsArray[1], out int intensity)
                && intensity >= 1 && intensity <= 25)
            {
                SomneoApiClient.EnableSunrisePreview(colorScheme, intensity);
                Console.WriteLine($"Previewing sunrise \"{EnumHelper.GetDescription(colorScheme)}\" with intensity {intensity}/25.");
                return;
            }
        }

        Console.WriteLine("The sunrise number should be between 0 and 2 with an intensity between 1 and 25.");
    }

    private void DisableSunrisePreview(string? args)
    {
        SomneoApiClient.DisableSunrisePreview();
        Console.WriteLine("Disabled sunrise preview.");
    }
}
