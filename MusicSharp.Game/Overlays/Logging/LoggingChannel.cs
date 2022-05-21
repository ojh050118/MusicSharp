using MusicSharp.Game.Graphics;
using MusicSharp.Game.Overlays.Logging.Channel;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace MusicSharp.Game.Overlays.Logging
{
    public class LoggingChannel : Container
    {
        public string ChannelName
        {
            get => header.Header;
            set => header.Header = value;
        }

        public string Description
        {
            get => header.Description;
            set => header.Description = value;
        }

        private ChannelHeader header;
        private LogScrollContainer log;

        public LoggingChannel()
        {
            header = new ChannelHeader
            {
                RelativeSizeAxes = Axes.X,
                Height = 80,
                Depth = -1
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
                new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.AutoSize),
                        new Dimension()
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            header
                        },
                        new Drawable[]
                        {
                            log = new LogScrollContainer
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        }
                    }
                }
            };
        }

        public void AddLog(LogMessage message) => Schedule(() => log?.AddLog(message));
    }
}
