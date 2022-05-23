using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Interactions;

namespace MusicSharp.Game.Commands.Music
{
    [Group("music", "music.")]
    public class Music : InteractionModuleBase
    {
        [SlashCommand("search", "유튜브에서 음악을 검색합니다.")]
        public async Task Search(string search)
        {

        }
    }
}
