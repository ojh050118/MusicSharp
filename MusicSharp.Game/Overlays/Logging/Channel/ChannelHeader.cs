using MusicSharp.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace MusicSharp.Game.Overlays.Logging.Channel
{
    public class ChannelHeader : Container
    {
        public string Header
        {
            get => headerText.Text.ToString();
            set => headerText.Text = value;
        }

        public string Description
        {
            get => descriptionText.Text.ToString();
            set
            {
                descriptionText.Text = value;
                descriptionChanged(value);
            }
        }

        private readonly SpriteText headerText;
        private readonly SpriteText descriptionText;

        private Box box;

        public const float PADDING = 20;

        public ChannelHeader()
        {
            headerText = new SpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Margin = new MarginPadding { Left = PADDING },
                Font = FontUsage.Default.With(family: "OpenSans-Bold", size: 28)
            };
            descriptionText = new SpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Margin = new MarginPadding { Left = PADDING },
                Alpha = 0.5f,
                Font = FontUsage.Default.With(size: 24)
            };
        }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours.Gray
                },
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(PADDING),
                    Child = new GridContainer
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        RelativeSizeAxes = Axes.Both,
                        RowDimensions = new[]
                        {
                            new Dimension(GridSizeMode.Distributed),
                        },
                        ColumnDimensions = new[]
                        {
                            new Dimension(GridSizeMode.AutoSize),
                            new Dimension(GridSizeMode.AutoSize),
                            new Dimension(GridSizeMode.AutoSize),
                        },
                        Content = new[]
                        {
                            new Drawable[]
                            {
                                new SpriteIcon
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Colour = colours.LightestGray,
                                    Icon = FontAwesome.Solid.Hashtag,
                                    Size = new Vector2(28)
                                },
                                headerText,
                                box = new Box
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Margin = new MarginPadding { Left = PADDING },
                                    RelativeSizeAxes = Axes.Y,
                                    Width = 2,
                                    Alpha = 0,
                                    Colour = colours.LightGray
                                },
                                descriptionText
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
            descriptionChanged(descriptionText.Text.ToString());
        }

        private void descriptionChanged(string text)
        {
            if (box == null)
                return;

            if (string.IsNullOrEmpty(text))
                box.Alpha = 0;
            else
                box.Alpha = 1;
        }
    }
}
