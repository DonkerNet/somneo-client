using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class ValuesCommandHandler : CommandHandlerBase
{
    public ValuesCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("wake-up-sounds", "Shows a list of available wake-up sounds used as parameters for other commands.", WakeUpSounds);
        commandRegistry.RegisterCommand("color-schemes", "Shows a list of available sunrise/sunset color schemes used as parameters for other commands.", ColorSchemes);
    }

    private void WakeUpSounds(string? args)
    {
        string wakeUpSoundList = string.Concat(Enum.GetValues<WakeUpSound>().Select(wus => $"{Environment.NewLine}  [{(int)wus}] {EnumHelper.GetDescription(wus)}"));
        Console.WriteLine($"Available wake-up sounds:{wakeUpSoundList}");
    }

    private void ColorSchemes(string? args)
    {
        string colorSchemeList = string.Concat(Enum.GetValues<ColorScheme>().Select(st => $"{Environment.NewLine}  [{(int)st}] {EnumHelper.GetDescription(st)}"));
        Console.WriteLine($"Available color schemes:{colorSchemeList}");
    }
}
