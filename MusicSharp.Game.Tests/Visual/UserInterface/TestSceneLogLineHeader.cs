using MusicSharp.Game.Overlays.Logging;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.UserInterface
{
    public class TestSceneLogLineHeader : MusicSharpTestScene
    {
        public TestSceneLogLineHeader()
        {
            Add(new LogLineHeader(new LogMessage("Hello, World!"))
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }
    }
}


