using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class AUXCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("enable-aux", "Enables the auxiliary input device.", EnableAUXAsync);
    }

    private async Task EnableAUXAsync(string? args)
    {
        await SomneoApiClient.EnableAUXAsync();
        Console.WriteLine("Enabled the auxiliary input device.");
    }
}
