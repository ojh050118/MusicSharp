using MusicSharp.Game.Overlays.Profile;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.UserInterface
{
    public class TestSceneClientInfo : MusicSharpTestScene
    {
        public TestSceneClientInfo()
        {
            Add(new ClientInfo
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Y,
                Width = 350
            });
        }
    }
}
