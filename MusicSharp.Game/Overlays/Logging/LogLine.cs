using MusicSharp.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;

namespace MusicSharp.Game.Overlays.Logging
{
    public class LogLine : LogLineContainer
    {
        private TextFlowContainer messageContent;
        private SpriteText createdTime;

        public LogLine(LogMessage message)
        {
            Message = message;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            Content.Child = new GridContainer
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
                        createdTime = new SpriteText
                        {
                            Alpha = 0,
                            Padding = new MarginPadding { Vertical = 2 },
                            Colour = colours.LightestGray,
                            Font = FontUsage.Default.With(size: 24),
                            Text = Message.CreatedTime.ToString("tt h:mm")
                        },
                        messageContent = new TextFlowContainer(t => t.Font = FontUsage.Default.With(size: 28))
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                        }
                    }
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            createdTime.Alpha = 1;

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            createdTime.Alpha = 0;
        }

        protected override void UpdateMessageContent() => messageContent.Text = Message?.Message;
    }
}
