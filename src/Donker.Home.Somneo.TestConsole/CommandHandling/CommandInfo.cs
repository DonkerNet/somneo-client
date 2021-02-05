using System;

namespace Donker.Home.Somneo.TestConsole.CommandHandling
{
    public class CommandInfo
    {
        public string Name { get; init; }
        public string ArgumentsDescription { get; init; }
        public string Description { get; init; }
        public Action<string> Handler { get; init; }
    }
}
