using System;
using System.Linq;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Helpers;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers
{
    public class ValuesCommandHandler : CommandHandlerBase
    {
        public ValuesCommandHandler(ISomneoApiClient somneoApiClient)
            : base(somneoApiClient)
        {
        }

        public override void RegisterCommands(CommandRegistry commandRegistry)
        {
            commandRegistry.RegisterCommand("wake-up-sounds", "Shows a list of available wake-up sounds used as parameters for other commands.", WakeUpSounds);
            commandRegistry.RegisterCommand("sunrise-types", "Shows a list of available sunrise types used as parameters for other commands.", SunriseTypes);
        }

        private void WakeUpSounds(string args)
        {
            string wakeUpSoundList = string.Concat(Enum.GetValues<WakeUpSound>().Select(wus => $"{Environment.NewLine}  [{(int)wus}] {EnumHelper.GetDescription(wus)}"));
            Console.WriteLine($"Available wake-up sounds:{wakeUpSoundList}");
        }

        private void SunriseTypes(string args)
        {
            string sunriseTypeList = string.Concat(Enum.GetValues<SunriseType>().Select(st => $"{Environment.NewLine}  [{(int)st}] {EnumHelper.GetDescription(st)}"));
            Console.WriteLine($"Available sunrise types:{sunriseTypeList}");
        }
    }
}
