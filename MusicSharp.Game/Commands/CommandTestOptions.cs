using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MusicSharp.Game.Commands
{
    public class CommandTestOptions : Command
    {
        public override string Name => "test-options";
        public override string Description => "Options test.";
        public override IReadOnlyList<SlashCommandOptionBuilder> Options => new[]
        {
            new SlashCommandOptionBuilder
            {
                Name = "number",
                Description = "number.",
                IsRequired = true,
                Type = ApplicationCommandOptionType.Integer
            },
        };

        public override async Task ExecutedCommand(SocketSlashCommand command)
        {
            var options = command.Data.Options.ToArray();
            await command.RespondAsync($"number: {options[0].Value}");
        }
    }
}
