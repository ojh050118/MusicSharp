using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MusicSharp.Game.Commands
{
    public interface ICommand : IEquatable<ICommand>
    {
        /// <summary>
        /// 명령어 이름입니다.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 명령어 설명입니다.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 이 명령어가 전역 명령어인지 여부.
        /// </summary>
        bool IsGlobalAppCommand { get; }

        /// <summary>
        /// 길드 명령어일 때 길드들. <see cref="IsGlobalAppCommand"/>가 활성화되어있을 때 무시됩니다.
        /// </summary>
        IReadOnlyList<ulong> SpecificGuilds { get; }

        /// <summary>
        /// 명령어 인수들.
        /// </summary>
        IReadOnlyList<SlashCommandOptionBuilder> Options { get; }

        /// <summary>
        /// 사용자가 명령어를 사용했을 때 호출됩니다.
        /// </summary>
        /// <param name="command">사용자가 사용한 명령어 정보.</param>
        /// <returns></returns>
        Task ExecutedCommand(SocketSlashCommand command);
    }
}
