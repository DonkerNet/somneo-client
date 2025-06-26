namespace Donker.Home.Somneo.TestConsole.CommandHandling;

public class CommandInfo(string name, string? argumentsDescription, string description, Action<string?> handler)
{
    public string Name { get; } = name;
    public string? ArgumentsDescription { get; } = argumentsDescription;
    public string Description { get; } = description;
    public Action<string?> Handler { get; } = handler;
}
