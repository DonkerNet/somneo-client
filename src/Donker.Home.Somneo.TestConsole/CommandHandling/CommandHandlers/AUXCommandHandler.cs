using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class AUXCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("enable-aux", "Enables the auxiliary input device.", EnableAUX);
    }

    private void EnableAUX(string? args)
    {
        SomneoApiClient.EnableAUX();
        Console.WriteLine("Enabled the auxiliary input device.");
    }
}
