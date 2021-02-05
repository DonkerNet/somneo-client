using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Donker.Home.Somneo.TestConsole.CommandHandling
{
    public class CommandRegistry
    {
        private readonly OrderedDictionary _commands;

        public int CommandCount => _commands.Count;
        
        public CommandRegistry()
        {
            _commands = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
        }

        public void RegisterCommand(string commandName, string argumentsDescription, string description, Action<string> commandHandler)
        {
            CommandInfo command = new CommandInfo
            {
                Name = commandName,
                ArgumentsDescription = argumentsDescription,
                Description = description,
                Handler = commandHandler
            };

            _commands.Add(commandName, command);
        }

        public void RegisterCommand(string commandName, string description, Action<string> commandHandler)
        {
            RegisterCommand(commandName, null, description, commandHandler);
        }

        public CommandInfo GetCommandInfo(string commandName)
        {
            if (_commands.Contains(commandName))
                return _commands[commandName] as CommandInfo;
            return null;
        }

        public IEnumerable<CommandInfo> Enumerate() => _commands.Values.Cast<CommandInfo>();
    }
}
