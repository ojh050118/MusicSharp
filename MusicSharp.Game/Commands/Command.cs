using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MusicSharp.Game.Commands
{
    public abstract class Command : SlashCommandBuilder, ICommand, IEquatable<Command>
    {
        public new abstract string Name { get; }

        public new abstract string Description { get; }

        /// <summary>
        /// 이 명령어가 전역 명령어인지 여부. 기본 값은 <see cref="true"/>입니다.
        /// </summary>
        public virtual bool IsGlobalAppCommand { get; } = true;

        public virtual IReadOnlyList<ulong> SpecificGuilds { get; }

        public new virtual IReadOnlyList<SlashCommandOptionBuilder> Options { get; } = new List<SlashCommandOptionBuilder>();

        /// <summary>
        /// 새로운 객체를 만듭니다.
        /// </summary>
        protected Command()
        {
            base.Name = Name;
            base.Description = Description;
            base.Options = Options.ToList();
        }

        public abstract Task ExecutedCommand(SocketSlashCommand command);

        public bool Equals(ICommand other) => other is Command them && Equals(them);

        public bool Equals(Command other) => Name == other.Name && Description == other.Description;
    }
}
