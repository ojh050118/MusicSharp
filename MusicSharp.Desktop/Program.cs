using MusicSharp.Game;
using osu.Framework;
using osu.Framework.Platform;

namespace MusicSharp.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"MusicSharp"))
            using (osu.Framework.Game game = new MusicSharpGame())
                host.Run(game);
        }
    }
}
