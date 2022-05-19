using MusicSharp.Game.Graphics;
using MusicSharp.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
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

        public bool Expanded
        {
            get => expanded;
            set
            {
                expanded = value;

                if (!IsLoaded)
                    return;

                updateMenuState();
            }
        }

        private bool expanded;

        private readonly SpriteText headerText;
        private SpriteIcon expandedIcon;

        private MusicSharpMenu menu;

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
                },
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding { Horizontal = 15 },
                    Child = menu = new MusicSharpMenu(Direction.Vertical)
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.X,
                        Margin = new MarginPadding { Top = 10 },
                        Items = new[]
                        {
                            new MusicSharpMenuItem("Alarm settings", FontAwesome.Solid.Bell, MenuItemType.Standard),
                            new MusicSharpMenuItem("Settings", FontAwesome.Solid.Cog, MenuItemType.Highlighted),
                            new MusicSharpMenuItem("Clear all logs", FontAwesome.Solid.TrashAlt, MenuItemType.Destructive),
                            new MusicSharpMenuItem("Exit", FontAwesome.Solid.DoorOpen, MenuItemType.Destructive),
                        }
                    }
                }
            };
            Action += () => Expanded = !expanded;
            menu.StateChanged += s =>
            {
                expanded = s == MenuState.Open;
                expandedIcon.Icon = s == MenuState.Closed ? FontAwesome.Solid.ChevronDown : FontAwesome.Solid.ChevronUp;
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateMenuState();
        }

        private void updateMenuState()
        {
            if (expanded)
            {
                expandedIcon.Icon = FontAwesome.Solid.ChevronUp;
                menu.Open();
                menu.FadeIn(100).ScaleTo(1, 300, Easing.OutQuint);
            }
            else
            {
                expandedIcon.Icon = FontAwesome.Solid.ChevronDown;
                menu.Close();
                menu.ScaleTo(0, 300, Easing.OutQuint).FadeOut(100);
            }
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
