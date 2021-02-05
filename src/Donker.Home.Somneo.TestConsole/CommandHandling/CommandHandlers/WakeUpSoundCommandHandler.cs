using System;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers
{
    public class WakeUpSoundCommandHandler : CommandHandlerBase
    {
        public WakeUpSoundCommandHandler(ISomneoApiClient somneoApiClient)
            : base(somneoApiClient)
        {
        }

        public override void RegisterCommands(CommandRegistry commandRegistry)
        {
            commandRegistry.RegisterCommand("play-wake-up-sound", "[1-8]", "Plays a wake-up sound.", PlayWakeUpSound);
        }

        private void PlayWakeUpSound(string args)
        {
            if (!string.IsNullOrEmpty(args) && Enum.TryParse(args, out WakeUpSound wakeUpSound))
            {
                SomneoApiClient.PlayWakeUpSound(wakeUpSound);
                Console.WriteLine($"Playing wake-up sound \"{EnumHelper.GetDescription(wakeUpSound)}\".");
                return;
            }

            Console.WriteLine("The wake-up sound number should be between 1 and 8.");
        }
    }
}
