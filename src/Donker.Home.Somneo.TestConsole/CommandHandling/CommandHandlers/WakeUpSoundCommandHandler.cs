using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;
using Donker.Home.Somneo.TestConsole.Helpers;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class WakeUpSoundCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("enable-wake-up-sound-preview", "[1-8] [1-25]", "Previews a wake-up sound with the specified volume.", EnableWakeUpSoundPreviewAsync);
        commandRegistry.RegisterCommand("disable-wake-up-sound-preview", null, "Disables the wake-up sound preview.", DisableWakeUpSoundPreviewAsync);
    }

    private async Task EnableWakeUpSoundPreviewAsync(string? args)
    {
        if (!string.IsNullOrEmpty(args))
        {
            string[] argsArray = args.Split(' ', 2);

            if (argsArray.Length == 2
                && int.TryParse(argsArray[0], out int wakeUpSoundNumber)
                && EnumHelper.TryCast(wakeUpSoundNumber - 1, out WakeUpSound wakeUpSound)
                && int.TryParse(argsArray[1], out int volume)
                && volume >= 1 && volume <= 25)
            {
                await SomneoApiClient.EnableWakeUpSoundPreviewAsync(wakeUpSound, volume);
                Console.WriteLine($"Previewing wake-up sound \"{EnumHelper.GetDescription(wakeUpSound)}\" with volume {volume}/25.");
                return;
            }
        }

        Console.WriteLine("The wake-up sound number should be between 1 and 8 with a volume between 1 and 25.");
    }

    private async Task DisableWakeUpSoundPreviewAsync(string? args)
    {
        await SomneoApiClient.DisableWakeUpSoundPreviewAsync();
        Console.WriteLine("Disabled wake-up sound preview.");
    }
}
