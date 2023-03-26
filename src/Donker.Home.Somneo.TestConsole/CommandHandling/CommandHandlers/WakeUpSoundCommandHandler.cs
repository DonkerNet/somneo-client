using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class WakeUpSoundCommandHandler : CommandHandlerBase
{
    public WakeUpSoundCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("enable-wake-up-sound-preview", "[1-8] [1-25]", "Previews a wake-up sound with the specified volume.", EnableWakeUpSoundPreview);
        commandRegistry.RegisterCommand("disable-wake-up-sound-preview", null, "Disables the wake-up sound preview.", DisableWakeUpSoundPreview);
    }

    private void EnableWakeUpSoundPreview(string? args)
    {
        if (!string.IsNullOrEmpty(args))
        {
            string[] argsArray = args.Split(new[] { ' ' }, 2);

            if (argsArray.Length == 2
                && Enum.TryParse(argsArray[0], out WakeUpSound wakeUpSound)
                && Enum.IsDefined(wakeUpSound)
                && int.TryParse(argsArray[1], out int volume)
                && volume >= 1 && volume <= 25)
            {
                SomneoApiClient.EnableWakeUpSoundPreview(wakeUpSound, volume);
                Console.WriteLine($"Previewing wake-up sound \"{EnumHelper.GetDescription(wakeUpSound)}\" with volume {volume}/25.");
                return;
            }
        }

        Console.WriteLine("The wake-up sound number should be between 1 and 8 with a volume between 1 and 25.");
    }

    private void DisableWakeUpSoundPreview(string? args)
    {
        SomneoApiClient.DisableWakeUpSoundPreview();
        Console.WriteLine("Disabled wake-up sound preview.");
    }
}
