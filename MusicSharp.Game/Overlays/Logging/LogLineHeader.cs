using MusicSharp.Game.Graphics;
using MusicSharp.Game.Online;
using MusicSharp.Game.Users.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace MusicSharp.Game.Overlays.Logging
{
    public class LogLineHeader : LogLineContainer
    {
        private TextFlowContainer messageContent;

        public LogLineHeader(LogMessage message)
        {
            Message = message;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours, DiscordClient client)
        {
            Content.Child = new GridContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize),
                },
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize, minSize: LEFT_CONTENT_SIZE),
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Size = new Vector2(56),
                            Padding = new MarginPadding(4),
                            Child = new CircularContainer
                            {
                                RelativeSizeAxes = Axes.Both,
                                Masking = true,
                                Child = new DelayedLoadWrapper(new DrawableAvatar(client.User)
                                {
                                    RelativeSizeAxes = Axes.Both
                                })
                                {
                                    RelativeSizeAxes = Axes.Both
                                }
                            }
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding { Left = PADDING },
                            Child = new GridContainer
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                ColumnDimensions = new[]
                                {
                                    new Dimension(GridSizeMode.Distributed),
                                    new Dimension(GridSizeMode.AutoSize)
                                },
                                RowDimensions = new[]
                                {
                                    new Dimension(GridSizeMode.AutoSize),
                                    new Dimension(GridSizeMode.AutoSize)
                                },
                                Content = new[]
                                {
                                    new Drawable[]
                                    {
                                        new GridContainer
                                        {
                                            RelativeSizeAxes = Axes.X,
                                            AutoSizeAxes = Axes.Y,
                                            ColumnDimensions = new[]
                                            {
                                                new Dimension(GridSizeMode.AutoSize),
                                                new Dimension(GridSizeMode.AutoSize),
                                            },
                                            RowDimensions = new[]
                                            {
                                                new Dimension(GridSizeMode.AutoSize)
                                            },
                                            Content = new[]
                                            {
                                                new Drawable[]
                                                {
                                                    new SpriteText
                                                    {
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        Font = FontUsage.Default.With(family: "OpenSans-Bold", size: 28),
                                                        Text = Message.Author
                                                    },
                                                    new SpriteText
                                                    {
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        Colour = colours.LightestGray,
                                                        Margin = new MarginPadding { Left = 10 },
                                                        Font = FontUsage.Default.With(size: 24),
                                                        Text = Message.CreatedTime.ToString("tt h:mm")
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new Drawable[]
                                    {
                                        messageContent = new TextFlowContainer(t => t.Font = FontUsage.Default.With(size: 28))
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            RelativeSizeAxes = Axes.X,
                                            AutoSizeAxes = Axes.Y,
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        protected override void UpdateMessageContent() => messageContent.Text = Message?.Message;
    }
}
