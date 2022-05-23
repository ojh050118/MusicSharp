using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace MusicSharp.Game.Commands
{
    public class CommmandVoiceChannel : Command
    {
        public override string Name => "join";

        public override string Description => "Enter the voice channel where the user is.";

        public override async Task ExecutedCommand(SocketSlashCommand command)
        {
            await command.
        }
    }
}
