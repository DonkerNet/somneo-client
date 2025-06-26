using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling;

public abstract class CommandHandlerBase(ISomneoApiClient somneoApiClient)
{
    protected ISomneoApiClient SomneoApiClient { get; } = somneoApiClient;

    public abstract void RegisterCommands(CommandRegistry commandRegistry);
}
