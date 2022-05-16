using MusicSharp.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;

namespace MusicSharp.Game.Overlays.Logging
{
    public class LogLine : Container
    {
        public const float MARGIN = 100;
        public const float PADDING = 20;

        public LogMessage Message
        {
            get => message;
            set
            {
                message = value;

                if (!IsLoaded)
                    return;

                updateMessageContent();
            }
        }

        private LogMessage message;

        private Box background;
        private TextFlowContainer messageContent;

        [Resolved]
        private DiscordColour colours { get; set; }

        public LogLine(LogMessage message)
        {
            Message = message;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours.Gray
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding { Left = PADDING, Right = MARGIN + PADDING },
                    Child = new GridContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        RowDimensions = new[]
                        {
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
                                new SpriteText
                                {
                                    //Anchor = Anchor.CentreLeft,
                                    //Origin = Anchor.CentreLeft,
                                    Padding = new MarginPadding { Vertical = 2 },
                                    Colour = colours.LightestGray,
                                    Font = FontUsage.Default.With(size: 24),
                                    Text = message.CreatedTime.ToString("tt h:mm")
                                },
                                messageContent = new TextFlowContainer(t => t.Font = FontUsage.Default.With(size: 28))
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                }
                            }
                        }
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateMessageContent();
        }

        protected override bool OnHover(HoverEvent e)
        {
            background.FadeColour(colours.DarkGray);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            background.FadeColour(colours.Gray);
        }

        private void updateMessageContent() => messageContent.Text = message?.Message;
    }
}
