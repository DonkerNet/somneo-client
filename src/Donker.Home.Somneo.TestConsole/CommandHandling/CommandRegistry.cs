using System.Collections.Specialized;

namespace Donker.Home.Somneo.TestConsole.CommandHandling;

public class CommandRegistry
{
    private readonly OrderedDictionary _commands;

    public int CommandCount => _commands.Count;
    
    public CommandRegistry()
    {
        _commands = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
    }

    public void RegisterCommand(string commandName, string? argumentsDescription, string description, Action<string?> commandHandler)
    {
        var command = new CommandInfo(
            commandName,
            argumentsDescription,
            description,
            commandHandler);

        _commands.Add(commandName, command);
    }

    public void RegisterCommand(string commandName, string description, Action<string?> commandHandler)
    {
        RegisterCommand(commandName, null, description, commandHandler);
    }

    public CommandInfo? GetCommandInfo(string commandName)
    {
        return _commands.Contains(commandName)
            ? _commands[commandName] as CommandInfo
            : null;
    }

    public IEnumerable<CommandInfo> Enumerate() => _commands.Values.Cast<CommandInfo>();
}
