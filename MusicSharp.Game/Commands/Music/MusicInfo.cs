using System;
using System.Collections.Generic;
using System.Text;

namespace MusicSharp.Game.Commands.Music
{
    public static class MusicInfo
    {
        public static Queue<string> Queue = new Queue<string>();

        public static string NowPlaying;
    }
}
