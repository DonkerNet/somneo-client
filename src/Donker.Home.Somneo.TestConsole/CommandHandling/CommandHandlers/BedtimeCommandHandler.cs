using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class BedtimeCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("start-bedtime", "Starts a new bedtime session.", StartBedtime);
        commandRegistry.RegisterCommand("end-bedtime", "Ends the current bedtime session.", EndBedtime);
        commandRegistry.RegisterCommand("last-bedtime", "Shows information about the last bedtime session.", LastBedtime);
    }

    private void StartBedtime(string? args)
    {
        SomneoApiClient.StartBedtime();
        Console.WriteLine("New bedtime session started.");
    }

    private void EndBedtime(string? args)
    {
        var bedtimeInfo = SomneoApiClient.EndBedtime();

        Console.WriteLine(
$@"Bedtime session ended:
  Start: {bedtimeInfo.Started}
  End: {bedtimeInfo.Ended}
  Duration: {bedtimeInfo.Duration}");
    }

    private void LastBedtime(string? args)
    {
        var bedtimeInfo = SomneoApiClient.GetLastBedtimeInfo();

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
