using System.Collections.Specialized;

namespace Donker.Home.Somneo.TestConsole.CommandHandling;

public class CommandRegistry
{
    private readonly OrderedDictionary _commands = new(StringComparer.OrdinalIgnoreCase);

    public int CommandCount => _commands.Count;

    public void RegisterCommand(string commandName, string? argumentsDescription, string description, Func<string?, Task> asyncHandler)
    {
        var command = new CommandInfo(
            commandName,
            argumentsDescription,
            description,
            asyncHandler);

        _commands.Add(commandName, command);
    }

    public void RegisterCommand(string commandName, string description, Func<string?, Task> asyncHandler)
    {
        RegisterCommand(commandName, null, description, asyncHandler);
    }

    public CommandInfo? GetCommandInfo(string commandName)
    {
        return _commands.Contains(commandName)
            ? _commands[commandName] as CommandInfo
            : null;
    }

    public IEnumerable<CommandInfo> Enumerate() => _commands.Values.Cast<CommandInfo>();
}
