using MusicSharp.Game.Graphics.UserInterface;
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
                RelativeSizeAxes = Axes.X,
                Height = 80
            });
        }
    }
}
