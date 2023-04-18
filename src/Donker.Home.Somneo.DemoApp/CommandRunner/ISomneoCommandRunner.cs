using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.DemoApp.CommandRunner;

public interface ISomneoCommandRunner
{
    Task Execute(Action<ISomneoApiClient> command);
    Task<TResult> Execute<TResult>(Func<ISomneoApiClient, TResult> command);
}
