using MusicSharp.Game.Graphics.UserInterface;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK.Graphics;

namespace MusicSharp.Game.Tests.Visual.UserInterface
{
    public class TestSceneMusicSharpMenu : MusicSharpTestScene
    {
        public TestSceneMusicSharpMenu()
        {
            Add(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Gray
            });
            Add(new MusicSharpMenu(Direction.Vertical, true)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Items = new[]
                {
                    new MusicSharpMenuItem("Standard", FontAwesome.Solid.Cog, MenuItemType.Standard),
                    new MusicSharpMenuItem("Hightlighted", FontAwesome.Solid.Cog, MenuItemType.Highlighted),
                    new MusicSharpMenuItem("Destructive", FontAwesome.Solid.Cog, MenuItemType.Destructive),
                }
            });
        }
    }
}


