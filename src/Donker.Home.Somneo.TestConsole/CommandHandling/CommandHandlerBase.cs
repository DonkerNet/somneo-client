using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling;

public abstract class CommandHandlerBase
{
    protected ISomneoApiClient SomneoApiClient { get; }

    protected CommandHandlerBase(ISomneoApiClient somneoApiClient)
    {
        SomneoApiClient = somneoApiClient;
    }

    public abstract void RegisterCommands(CommandRegistry commandRegistry);
}
