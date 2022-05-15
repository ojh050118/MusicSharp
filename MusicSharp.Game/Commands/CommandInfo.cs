using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MusicSharp.Game.Commands
{
    public class CommandInfo : Command
    {
        public override string Name => "info";
        public override string Description => "Return bot info";

        public override async Task ExecutedCommand(SocketSlashCommand command)
        {
            await command.RespondAsync("Running on Discord.NET v3.6.1 (API v9)");
        }
    }
}
