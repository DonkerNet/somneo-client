using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class BedtimeCommandHandler : CommandHandlerBase
{
    public BedtimeCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

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
        BedtimeInfo bedtimeInfo = SomneoApiClient.EndBedtime();

        Console.WriteLine(
$@"Bedtime session ended:
  Start: {bedtimeInfo.Started}
  End: {bedtimeInfo.Ended}
  Duration: {bedtimeInfo.Duration}");
    }

    private void LastBedtime(string? args)
    {
        BedtimeInfo? bedtimeInfo = SomneoApiClient.GetLastBedtimeInfo();

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
