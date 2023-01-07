using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

public class TimerCommandHandler : CommandHandlerBase
{
    public TimerCommandHandler(ISomneoApiClient somneoApiClient)
        : base(somneoApiClient)
    {
    }

    public override void RegisterCommands(CommandRegistry commandRegistry)
    {
        commandRegistry.RegisterCommand("timer-state", "Get the state of the timer, used for RelaxBreathe or sunset.", GetTimerState);
    }

    private void GetTimerState(string? args)
    {
        TimerState timerState = SomneoApiClient.GetTimerState();

        if (timerState == null)
        {
            Console.WriteLine("Unable to retrieve the timer state.");
            return;
        }

        if (!timerState.Enabled)
        {
            Console.WriteLine("Timer state: Disabled");
            return;
        }

        string enabledFor = timerState.RelaxBreatheEnabled ? "RelaxBreathe" : "sunset";
        TimeSpan duration = timerState.RelaxBreatheTime ?? timerState.SunsetTime!.Value;
        DateTimeOffset startTime = timerState.StartTime!.Value;
        DateTimeOffset currentTime = DateTimeOffset.UtcNow.ToOffset(startTime.Offset);

        Console.WriteLine(
$@"Timer state: Enabled for {enabledFor}
  Started at: {timerState.StartTime}
  Total duration: {duration}
  Time left: {currentTime - startTime}");
    }
}
