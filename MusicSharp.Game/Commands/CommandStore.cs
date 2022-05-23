using System.Collections.Generic;
using Discord;
using Discord.WebSocket;

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
                new CommmandJoin(),
                new CommandLeave(),
            };
        }

        public IEnumerable<SlashCommandProperties> GetBuildedCommands()
        {

            foreach (var command in GetCommands())
                yield return command.Build();
        }
    }
}
