using MusicSharp.Game.Overlays.Logging;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.Logging
{
    public class TestSceneLogLineHeader : MusicSharpTestScene
    {
        public TestSceneLogLineHeader()
        {
            Add(new LogLineHeader(new LogMessage("Discord", "Hello, World!"))
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }
    }
}


