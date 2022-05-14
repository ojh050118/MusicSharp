using MusicSharp.Game.Graphics.UserInterface;
using osu.Framework.Graphics;
using osuTK;
using osuTK.Graphics;

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
                Size = new Vector2(400, 100)
            });
        }
    }
}
