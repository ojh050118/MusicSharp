using MusicSharp.Game.Overlays.Profile;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.UserInterface
{
    public class TestSceneSettingsToolboxGroup : MusicSharpTestScene
    {
        public TestSceneSettingsToolboxGroup()
        {
            Add(new SettingsToolboxGroup
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Header = "MusicSharp",
                RelativeSizeAxes = Axes.X,
                Height = 80
            });
        }
    }
}


