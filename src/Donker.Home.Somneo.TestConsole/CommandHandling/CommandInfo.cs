namespace Donker.Home.Somneo.TestConsole.CommandHandling;

public class CommandInfo
{
    public string Name { get; }
    public string? ArgumentsDescription { get; }
    public string Description { get; }
    public Action<string?> Handler { get; }

    public CommandInfo(string name, string? argumentsDescription, string description, Action<string?> handler)
    {
        Name = name;
        ArgumentsDescription = argumentsDescription;
        Description = description;
        Handler = handler;
    }
}
