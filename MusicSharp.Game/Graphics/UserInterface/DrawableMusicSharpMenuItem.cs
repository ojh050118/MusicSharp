using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using osuTK.Graphics;

namespace MusicSharp.Game.Graphics.UserInterface
{
    public class DrawableMusicSharpMenuItem : Menu.DrawableMenuItem
    {
        public const int MARGIN_HORIZONTAL = 10;
        public const int MARGIN_VERTICAL = 5;
        private const int text_size = 28;

        private TextIconContainer text;

        [Resolved]
        private DiscordColour colours { get; set; }

        public DrawableMusicSharpMenuItem(MenuItem item)
            : base(item)
        {
            Foreground.AutoSizeAxes = Axes.None;
        }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            BackgroundColour = Color4.Transparent;
            BackgroundColourHover = colours.Blue;
            Masking = true;
            CornerRadius = 4f;
            
            Foreground.RelativeSizeAxes = Axes.X;
            Foreground.AutoSizeAxes = Axes.Y;

            updateTextColour();

            Item.Action.BindDisabledChanged(_ => updateState(), true);
        }

        private void updateTextColour()
        {
            switch ((Item as MusicSharpMenuItem)?.Type)
            {
                default:
                case MenuItemType.Standard:
                    text.Colour = colours.DarkWhite;
                    break;

                case MenuItemType.Destructive:
                    text.Colour = colours.Red;
                    BackgroundColourHover = colours.Red;
                    break;

                case MenuItemType.Highlighted:
                    text.Colour = colours.LightBlue;
                    break;
            }

            if ((Item as MusicSharpMenuItem)?.Icon != null)
                text.Icon = ((MusicSharpMenuItem)Item).Icon;
        }

        protected override bool OnHover(HoverEvent e)
        {
            updateState();
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            updateState();
            base.OnHoverLost(e);
        }

        private void updateState()
        {
            Alpha = Item.Action.Disabled ? 0.2f : 1;

            if (IsHovered && !Item.Action.Disabled)
            {
                text.Colour = Color4.White;
            }
            else
            {
                updateTextColour();
            }
        }

        protected sealed override Drawable CreateContent() => text = CreateTextIconContainer();
        protected virtual TextIconContainer CreateTextIconContainer() => new TextIconContainer();

        protected class TextIconContainer : Container, IHasText
        {
            public LocalisableString Text
            {
                get => text.Text;
                set => text.Text = value;
            }

            public IconUsage Icon
            {
                get => icon.Icon;
                set => icon.Icon = value;
            }

            private readonly SpriteText text;
            private readonly SpriteIcon icon;

            // Todo: 아이템이 크기 상대성에 따라 올바르게 표시되어야함.
            public TextIconContainer()
            {
                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                AutoSizeAxes = Axes.Y;
                RelativeSizeAxes = Axes.X;
                Padding = new MarginPadding { Horizontal = MARGIN_HORIZONTAL, Vertical = MARGIN_VERTICAL };

                Child = new GridContainer
                {
                    AlwaysPresent = true,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    ColumnDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Distributed),
                        new Dimension(GridSizeMode.AutoSize),
                    },
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.AutoSize),
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            text = new SpriteText
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Font = FontUsage.Default.With(size: text_size),
                                RelativeSizeAxes = Axes.X,
                                Truncate = true
                            },
                            icon = new SpriteIcon
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Size = new Vector2(text_size - 8)
                            }
                        }
                    }
                };
            }
        }
    }
}
