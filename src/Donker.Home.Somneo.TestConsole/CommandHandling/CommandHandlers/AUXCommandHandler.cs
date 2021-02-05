using System;
using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers
{
    public class AUXCommandHandler : CommandHandlerBase
    {
        public AUXCommandHandler(ISomneoApiClient somneoApiClient)
            : base(somneoApiClient)
        {
        }

        public override void RegisterCommands(CommandRegistry commandRegistry)
        {
            commandRegistry.RegisterCommand("enable-aux", "Enables the auxiliary input device.", EnableAUX);
        }

        private void EnableAUX(string args)
        {
            SomneoApiClient.EnableAUX();
            Console.WriteLine("Enabled the auxiliary input device.");
        }
    }
}
