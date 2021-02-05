using System;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.ApiClient.Models;

namespace Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers
{
    public class DisplayCommandHandler : CommandHandlerBase
    {
        public DisplayCommandHandler(ISomneoApiClient somneoApiClient)
            : base(somneoApiClient)
        {
        }

        public override void RegisterCommands(CommandRegistry commandRegistry)
        {
            commandRegistry.RegisterCommand("display", "Show the display state.", ShowDisplayState);
            commandRegistry.RegisterCommand("toggle-permanent-display", "[on/off]", "Toggle the permanent display.", TogglePermanentDisplay);
            commandRegistry.RegisterCommand("set-display-level", "[1-6]", "Set the display level.", SetDisplayLevel);
        }

        private void ShowDisplayState(string args)
        {
            DisplayState displayState = SomneoApiClient.GetDisplayState();

            if (displayState == null)
            {
                Console.WriteLine("Unable to retrieve the display state.");
                return;
            }

            Console.WriteLine(
$@"Display state:
  Permanent display enabled: {(displayState.Permanent ? "Yes" : "No")}
  Brightness level: {displayState.Brightness}/6");
        }

        private void TogglePermanentDisplay(string args)
        {
            switch (args?.ToLower())
            {
                case "on":
                    SomneoApiClient.TogglePermanentDisplay(true);
                    Console.WriteLine("Permanent display enabled.");
                    break;

                case "off":
                    SomneoApiClient.TogglePermanentDisplay(false);
                    Console.WriteLine("Permanent display disabled.");
                    break;

                default:
                    Console.WriteLine("Specify \"on\" or \"off\".");
                    break;
            }
        }

        private void SetDisplayLevel(string args)
        {
            if (!string.IsNullOrEmpty(args) && int.TryParse(args, out int level) && level >= 1 && level <= 6)
            {
                SomneoApiClient.SetDisplayLevel(level);
                Console.WriteLine($"Display brightness level set to {level}/6.");
                return;
            }

            Console.WriteLine("Specify a light level between 1 and 6.");
        }
    }
}
