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
                    new Dimension(GridSizeMode.AutoSize),
                    new Dimension(GridSizeMode.AutoSize)
                },
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize, minSize: MARGIN),
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new Container
                        {
                            Size = new Vector2(56),
                            Padding = new MarginPadding { Vertical = 4, Right = 8 },
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
                        new GridContainer
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
                                    new FillFlowContainer
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        AutoSizeAxes = Axes.Y,
                                        Direction = FillDirection.Horizontal,
                                        Spacing = new Vector2(10),
                                        Children = new Drawable[]
                                        {
                                            new SpriteText
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Padding = new MarginPadding { Vertical = 2 },
                                                Font = FontUsage.Default.With(family: "OpenSans-Bold", size: 24),
                                                Text = "Discord"
                                            },
                                            new SpriteText
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Padding = new MarginPadding { Vertical = 2 },
                                                Colour = colours.LightestGray,
                                                Font = FontUsage.Default.With(size: 24),
                                                Text = Message.CreatedTime.ToString("tt h:mm")
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
            };
        }

        protected override void UpdateMessageContent() => messageContent.Text = Message?.Message;
    }
}
