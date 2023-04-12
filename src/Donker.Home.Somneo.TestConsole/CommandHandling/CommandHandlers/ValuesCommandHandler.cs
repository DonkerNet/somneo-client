using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

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
        commandRegistry.RegisterCommand("sunset-sounds", "Shows a list of available sunset sounds used as parameters for other commands.", SunsetSounds);
        commandRegistry.RegisterCommand("color-schemes", "Shows a list of available sunrise/sunset color schemes used as parameters for other commands.", ColorSchemes);
    }

    private void WakeUpSounds(string? args)
    {
        string wakeUpSoundList = string.Concat(Enum.GetValues<WakeUpSound>().Select(wus => $"{Environment.NewLine}  [{(int)wus + 1}] {EnumHelper.GetDescription(wus)}"));
        Console.WriteLine($"Available wake-up sounds:{wakeUpSoundList}");
    }

    private void SunsetSounds(string? args)
    {
        string sunsetSoundList = string.Concat(Enum.GetValues<SunsetSound>().Select(ss => $"{Environment.NewLine}  [{(int)ss + 1}] {EnumHelper.GetDescription(ss)}"));
        Console.WriteLine($"Available sunset sounds:{sunsetSoundList}");
    }

    private void ColorSchemes(string? args)
    {
        string colorSchemeList = string.Concat(Enum.GetValues<ColorScheme>().Select(st => $"{Environment.NewLine}  [{(int)st + 1}] {EnumHelper.GetDescription(st)}"));
        Console.WriteLine($"Available color schemes:{colorSchemeList}");
    }
}
