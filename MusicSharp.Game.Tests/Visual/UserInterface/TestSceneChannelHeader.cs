using MusicSharp.Game.Overlays.Log.Channel;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.UserInterface
{
    public class TestSceneChannelHeader : MusicSharpTestScene
    {
        public TestSceneChannelHeader()
        {
            Add(new ChannelHeader
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Height = 80,
                Header = "Log-channel",
                Description = "Test channel"
            });
        }
    }
}
