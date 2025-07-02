using Donker.Home.Somneo.ApiClient;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class TimerCommandHandler(ISomneoApiClient somneoApiClient) : CommandHandlerBase(somneoApiClient)
{
    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("timer-state", "Get the state of the timer, used for RelaxBreathe or sunset.", GetTimerStateAsync);
    }

    private async Task GetTimerStateAsync(string? args)
    {
        var timerState = await SomneoApiClient.GetTimerStateAsync();

        if (!timerState.Enabled)
        {
            Console.WriteLine("Timer state: Disabled");
            return;
        }

        string enabledFor = timerState.RelaxBreatheEnabled ? "RelaxBreathe" : "sunset";
        var duration = timerState.RelaxBreatheTime ?? timerState.SunsetTime!.Value;
        var startTime = timerState.StartTime!.Value;
        var currentTime = DateTimeOffset.UtcNow.ToOffset(startTime.Offset);

        Console.WriteLine(
$@"Timer state: Enabled for {enabledFor}
  Started at: {timerState.StartTime}
  Total duration: {duration}
  Time left: {currentTime - startTime}");
    }
}
