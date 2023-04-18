using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.DemoApp.CommandRunner;

/*
 * Asynchronous requests may not work well with the Somneo device,
 * so we use this class to make one request at a time in an async context.
 */

public class SomneoCommandRunner : ISomneoCommandRunner
{
    private readonly ISomneoApiClient _somneoApiClient;
    private readonly SemaphoreSlim _syncRoot;

    public SomneoCommandRunner(ISomneoApiClient somneoApiClient)
    {
        _somneoApiClient = somneoApiClient;
        _syncRoot = new SemaphoreSlim(1);
    }

    public Task Execute(Action<ISomneoApiClient> command)
    {
        return Execute(somneo =>
        {
            command(somneo);
            return true;
        });
    }

    public async Task<TResult> Execute<TResult>(Func<ISomneoApiClient, TResult> command)
    {
        await _syncRoot.WaitAsync();

        try
        {
            return command.Invoke(_somneoApiClient);
        }
        finally
        {
            _syncRoot.Release();
        }
    }
}
