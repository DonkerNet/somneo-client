namespace Donker.Home.Somneo.TestConsole.CommandHandling;

public class CommandInfo(string name, string? argumentsDescription, string description, Func<string?, Task> asyncHandler)
{
    public string Name { get; } = name;
    public string? ArgumentsDescription { get; } = argumentsDescription;
    public string Description { get; } = description;
    public Func<string?, Task> AsyncHandler { get; } = asyncHandler;
}
