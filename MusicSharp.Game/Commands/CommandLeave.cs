using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MusicSharp.Game.Commands
{
    public class CommandLeave : Command
    {
        public override string Name => "leave";

        public override string Description => "연결되어있는 음성채널을 떠납니다.";

        public override async Task ExecutedCommand(SocketSlashCommand command)
        {
            var response = new EmbedBuilder
            {
                Title = ":white_check_mark: Leave",
                Description = $"음성채널을 떠났습니다. ({(command.Channel is IGroupChannel)})",
                Color = Color.Green
            }.Build();

            await (command.User as IVoiceState).VoiceChannel?.DisconnectAsync();

            await command.RespondAsync(embed: response);
        }
    }
}
