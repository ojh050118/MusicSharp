using System.Collections.Generic;
using Discord;

namespace MusicSharp.Game.Commands
{
    public class CommandStore
    {
        public IEnumerable<Command> GetCommands()
        {
            return new Command[]
            {
                new CommandInfo(),
                new CommandTestOptions(),
            };
        }

        public IEnumerable<SlashCommandProperties> GetBuildedCommands()
        {

            foreach (var command in GetCommands())
                yield return command.Build();
        }
    }
}
