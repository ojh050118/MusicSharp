using MusicSharp.Game.Overlays.Logging.Channel;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.Channel
{
    public class TestSceneChannelButton : MusicSharpTestScene
    {
        public TestSceneChannelButton()
        {
            Add(new ChannelButton
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Height = 60,
                ChannelName = "osu-framework",
                Action = Show
            });
        }
    }
}


