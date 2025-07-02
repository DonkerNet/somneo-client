using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class BedtimeCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("start-bedtime", "Starts a new bedtime session.", StartBedtimeAsync);
        commandRegistry.RegisterCommand("end-bedtime", "Ends the current bedtime session.", EndBedtimeAsync);
        commandRegistry.RegisterCommand("last-bedtime", "Shows information about the last bedtime session.", LastBedtimeAsync);
    }

    private async Task StartBedtimeAsync(string? args)
    {
        await SomneoApiClient.StartBedtimeAsync();
        Console.WriteLine("New bedtime session started.");
    }

    private async Task EndBedtimeAsync(string? args)
    {
        var bedtimeInfo = await SomneoApiClient.EndBedtimeAsync();

        Console.WriteLine(
$@"Bedtime session ended:
  Start: {bedtimeInfo.Started}
  End: {bedtimeInfo.Ended}
  Duration: {bedtimeInfo.Duration}");
    }

    private async Task LastBedtimeAsync(string? args)
    {
        var bedtimeInfo = await SomneoApiClient.GetLastBedtimeInfoAsync();

        if (bedtimeInfo == null)
        {
            Console.WriteLine("No recent bedtime info.");
            return;
        }

        Console.WriteLine(
$@"Last bedtime info:
  Start: {bedtimeInfo.Started}
  End: {bedtimeInfo.Ended}
  Duration: {bedtimeInfo.Duration}");
    }
}
