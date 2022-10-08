using System;
using Donker.Home.Somneo.ApiClient;
using Donker.Home.Somneo.TestConsole.CommandHandling;
using Donker.Home.Somneo.TestConsole.CommandHandling.CommandHandlers;

namespace Donker.Home.Somneo.TestConsole
{
    public class TestService
    {
        private readonly CommandRegistry _commandRegistry;

        private bool _canRun;

        public TestService(ISomneoApiClient somneoApiClient)
        {
            _commandRegistry = new CommandRegistry();

            var commandHandlers = new CommandHandlerBase[]
            {
                new ValuesCommandHandler(somneoApiClient),
                new DeviceCommandHandler(somneoApiClient),
                new SensorCommandHandler(somneoApiClient),
                new LightCommandHandler(somneoApiClient),
                new DisplayCommandHandler(somneoApiClient),
                new PlayerCommandHandler(somneoApiClient),
                new FMRadioCommandHandler(somneoApiClient),
                new WakeUpSoundCommandHandler(somneoApiClient),
                new AUXCommandHandler(somneoApiClient),
                new AlarmCommandHandler(somneoApiClient),
                new TimerCommandHandler(somneoApiClient),
                new SunsetCommandHandler(somneoApiClient)
            };

            _commandRegistry.RegisterCommand("help", "Show available commands.", ShowHelp);

            foreach (var commandHandler in commandHandlers)
                commandHandler.RegisterCommands(_commandRegistry);

            _commandRegistry.RegisterCommand("exit", "Exit the application.", args => _canRun = false);
        }

        public void Run()
        {
            _canRun = true;

            Console.WriteLine(
@"Welcome to the Somneo API client test console.
Type ""help"" to get started.");

            do
            {
                Console.WriteLine();
                Console.Write("> ");
                string command = Console.ReadLine();
                Console.WriteLine();
                RunCommand(command);
            }
            while (_canRun);

            Console.WriteLine();
            Console.WriteLine("Bye!");
        }

        private void RunCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return;

            string[] parts = command.Split(new[] { ' ' }, 2);

            var commandInfo = _commandRegistry.GetCommandInfo(parts[0]);

            if (commandInfo == null)
            {
                Console.WriteLine($"Invalid input: \"{command}\"");
                Console.WriteLine("Type \"help\" to show available commands.");
                return;
            }

            string commandHandlerArguments = parts.Length > 1 ? parts[1] : null;
            
            try
            {
                commandInfo.Handler(commandHandlerArguments);
            }
            catch (SomneoApiException ex)
            {
                Console.WriteLine("Somneo error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex);
            }
        }

        #region Command handler methods

        private void ShowHelp(string args)
        {
            Console.WriteLine("Available commands:");

            const int pageSize = 8;
            int commandCount = _commandRegistry.CommandCount;
            int counter = 0;

            foreach (CommandInfo commandInfo in _commandRegistry.Enumerate())
            {
                Console.WriteLine(@$"
{commandInfo.Name} {commandInfo.ArgumentsDescription}
  {commandInfo.Description}");

                ++counter;

                if (counter % pageSize == 0 && counter < commandCount)
                {
                    Console.WriteLine(@$"
{counter}/{commandCount} command(s) shown. [C]ontinue or [S]top.");

                    bool requestInput = true;

                    while (requestInput)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.C:
                                requestInput = false;
                                break;
                            case ConsoleKey.S:
                                Console.WriteLine("Command list stopped.");
                                return;
                        }
                    }

                    Console.WriteLine("Available commands (continued):");
                }
            }

            Console.WriteLine(@"
Command list finished.");
        }

        #endregion
    }
}
