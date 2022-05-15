using MusicSharp.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace MusicSharp.Game.Overlays.Profile
{
    public class SettingsToolboxGroup : ClickableContainer
    {
        public string Header
        {
            get => headerText.Text.ToString();
            set => headerText.Text = value;
        }

        private SpriteText headerText;
        private SpriteIcon expandedIcon;

        private Box background;

        [Resolved]
        private DiscordColour colours { get; set; }

        public SettingsToolboxGroup()
        {
            headerText = new SpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                RelativeSizeAxes = Axes.X,
                Truncate = true,
                Font = FontUsage.Default.With(family: "OpenSans-Bold", size: 28)
            };
        }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours.DarkGray
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(20),
                    Child = new GridContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        ColumnDimensions = new[]
                        {
                            new Dimension(GridSizeMode.Distributed),
                            new Dimension(GridSizeMode.AutoSize)
                        },
                        Content = new[]
                        {
                            new Drawable[]
                            {
                                headerText,
                                expandedIcon = new SpriteIcon
                                {
                                    Anchor = Anchor.CentreRight,
                                    Origin = Anchor.CentreRight,
                                    Size = new Vector2(28),
                                    Margin = new MarginPadding { Left = 20 },
                                    Icon = FontAwesome.Solid.ChevronDown,
                                    Colour = colours.LightestGray
                                }
                            }
                        }
                    }
                },
                new Box
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 3,
                    Colour = ColourInfo.GradientVertical(Color4.Black.Opacity(0.5f), Color4.Black.Opacity(0)),
                    Anchor = Anchor.BottomLeft
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            background.FadeColour(colours.LightGray, 200);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            background.FadeColour(colours.DarkGray, 200);
        }
    }
}
