using MusicSharp.Game.Overlays.Logging;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.Logging
{
    public class TestSceneLogLine : MusicSharpTestScene
    {
        public TestSceneLogLine()
        {
            Add(new LogLine(new LogMessage("Discord", "Hello, World!"))
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }
    }
}


