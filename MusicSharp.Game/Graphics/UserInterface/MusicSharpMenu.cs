using MusicSharp.Game.Graphics.Containers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace MusicSharp.Game.Graphics.UserInterface
{
    public class MusicSharpMenu : Menu
    {
        [Resolved]
        private DiscordColour colours { get; set; }

        public MusicSharpMenu(Direction direction, bool topLevelMenu = false)
            : base(direction, topLevelMenu)
        {
            MaskingContainer.CornerRadius = 5;
            ItemsContainer.Padding = new MarginPadding(10);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            BackgroundColour = colours.DarkestGray;
        }

        protected override void AnimateOpen()
        {
            this.FadeIn(300, Easing.OutQuint);
        }

        protected override void AnimateClose()
        {
            this.FadeOut(300, Easing.OutQuint);
        }

        protected override void UpdateSize(Vector2 newSize)
        {
            if (Direction == Direction.Vertical)
            {
                Width = newSize.X + (RelativeSizeAxes == Axes.X ? 0 : 20);
                this.ResizeHeightTo(newSize.Y, 300, Easing.OutQuint);
            }
            else
            {
                Height = newSize.Y;
                this.ResizeWidthTo(newSize.X + (RelativeSizeAxes == Axes.X ? 0 : 20), 300, Easing.OutQuint);
            }
        }

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item)
        {
            switch (item)
            {
                case StatefulMenuItem stateful:
                    return new DrawableStatefulMenuItem(stateful);

                case MusicSharpMenuItem defaultItem:
                    return new DrawableMusicSharpMenuItem(defaultItem);
            }

            return new DrawableMusicSharpMenuItem(item);
        }

        protected override ScrollContainer<Drawable> CreateScrollContainer(Direction direction) => new MusicSharpScrollContainer(direction);

        protected override Menu CreateSubMenu() => new MusicSharpMenu(Direction.Vertical)
        {
            Anchor = Direction == Direction.Horizontal ? Anchor.BottomLeft : Anchor.TopRight
        };
    }
}
