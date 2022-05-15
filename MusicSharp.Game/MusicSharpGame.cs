using MusicSharp.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;

namespace MusicSharp.Game
{
    public class MusicSharpGame : MusicSharpGameBase
    {
        private ScreenStack screenStack;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Distributed, maxSize: 350),
                    new Dimension(GridSizeMode.Distributed),
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new ClientInfo
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 350
                        },
                        screenStack = new ScreenStack
                        {
                            RelativeSizeAxes = Axes.Both,
                        }
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(new MainScreen());
        }
    }
}
