using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MusicSharp.Game.Commands
{
    public class CommmandJoin : Command
    {
        public override string Name => "join";

        public override IReadOnlyList<SlashCommandOptionBuilder> Options => new[]
        {
            new SlashCommandOptionBuilder
            {
                ChannelTypes = new List<ChannelType>
                {
                    ChannelType.Voice
                },
                Type = ApplicationCommandOptionType.Channel,
                Name = "channel",
                Description = "음성채널 이름",
                IsRequired = false,
            }
        };

        public override string Description => "음성채널에 입장합니다.";

        public override async Task ExecutedCommand(SocketSlashCommand command)
        {
            var options = command.Data.Options.ToArray();
            var isUserVoiceChannel = (command.User as IVoiceState).VoiceChannel != null;

            var response = new EmbedBuilder
            {
                Title = ":white_check_mark: Join",
                Description = "음성채널에 입장했습니다!",
                Color = Color.Green
            };

            if (!isUserVoiceChannel)
            {
                response.Title = ":warning: Join";
                response.Description = "음성채널에 연결되어있지 않습니다.";
                response.Color = Color.Gold;
            }

            if (options.Length == 0)
            {
                if (isUserVoiceChannel)
                    await (command.User as IVoiceState).VoiceChannel.ConnectAsync(true);
            }
            else
                await (options[0].Value as IVoiceChannel).ConnectAsync(true);

            await command.RespondAsync(embed: response.Build());
        }
    }
}
