using MusicSharp.Game.Overlays.Logging;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.UserInterface
{
    public class TestSceneLogLine : MusicSharpTestScene
    {
        public TestSceneLogLine()
        {
            Add(new LogLine(new LogMessage("Hello, World!"))
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }
    }
}


