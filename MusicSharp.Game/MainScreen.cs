using System.Linq;
using MusicSharp.Game.Graphics;
using MusicSharp.Game.Graphics.UserInterface;
using MusicSharp.Game.Online;
using MusicSharp.Game.Overlays.Logging;
using MusicSharp.Game.Overlays.Logging.Channel;
using MusicSharp.Game.Overlays.Profile;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;

namespace MusicSharp.Game
{
    public class MainScreen : Screen
    {
        private DiscordClient client;

        private TextButton stopButton;
        private TextButton startButton;

        private ClientInfo clientInfo;
        private Container<LoggingChannel> loggingContainer;

        [BackgroundDependencyLoader]
        private void load(DiscordColour colour, DiscordClient client)
        {
            InternalChild = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Distributed, maxSize: 350),
                    new Dimension(GridSizeMode.Distributed),
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        clientInfo = new ClientInfo
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 350
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Colour = colour.Gray,
                                    RelativeSizeAxes = Axes.Both,
                                },
                                new GridContainer
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    RowDimensions = new[]
                                    {
                                        new Dimension(),
                                        new Dimension(GridSizeMode.AutoSize),
                                    },
                                    Content = new[]
                                    {
                                        new Drawable[]
                                        {
                                            loggingContainer = new Container<LoggingChannel>
                                            {
                                                RelativeSizeAxes = Axes.Both,
                                            }
                                        },
                                        new Drawable[]
                                        {
                                            new GridContainer
                                            {
                                                Anchor = Anchor.BottomCentre,
                                                Origin = Anchor.BottomCentre,
                                                RelativeSizeAxes = Axes.X,
                                                AutoSizeAxes = Axes.Y,
                                                RowDimensions = new[]
                                                {
                                                    new Dimension(GridSizeMode.AutoSize),
                                                    new Dimension(GridSizeMode.AutoSize),
                                                },
                                                Content = new[]
                                                {
                                                    new Drawable[]
                                                    {
                                                        stopButton = new TextButton
                                                        {
                                                            Anchor = Anchor.Centre,
                                                            Origin = Anchor.Centre,
                                                            RelativeSizeAxes = Axes.X,
                                                            Height = 60,
                                                            Padding = new MarginPadding(5),
                                                            Colour = colour.Red,
                                                            LightenOnHover = true,
                                                            Text = "Stop",
                                                            Action = stop
                                                        },
                                                        startButton = new TextButton
                                                        {
                                                            Anchor = Anchor.Centre,
                                                            Origin = Anchor.Centre,
                                                            RelativeSizeAxes = Axes.X,
                                                            Height = 60,
                                                            Colour = colour.Blue,
                                                            Padding = new MarginPadding(5),
                                                            Text = "Start",
                                                            LightenOnHover = true,
                                                            Action = start
                                                        },
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            this.client = client;
            client.IsRunning.ValueChanged += v =>
            {
                stopButton.Enabled.Value = v.NewValue;
                startButton.Enabled.Value = !v.NewValue;
            };
            client.IsRunning.TriggerChange();

            clientInfo.ChannelCollection.OnChanged.ValueChanged += onChannelChanged;
        }

        private void onChannelChanged(ValueChangedEvent<RadioButton> e)
        {
            var lastChannel = loggingContainer.Children.SingleOrDefault(c => c.ChannelName == e.OldValue.Label);
            LoggingChannel channel;

            if ((channel = loggingContainer.Children.SingleOrDefault(c => c.ChannelName == e.NewValue.Label)) != null)
            {
                loggingContainer.ChangeChildDepth(channel, lastChannel?.Depth - 1 ?? 0);
            }
            else
            {
                channel = new LoggingChannel
                {
                    RelativeSizeAxes = Axes.Both,
                    ChannelName = e.NewValue.Label,
                    Description = e.NewValue.Description
                };
                loggingContainer.Add(channel);
            }
        }

        private void start()
        {
            if (client != null)
                client.Start();
        }

        private async void stop()
        {
            if (client != null)
                await client.Stop();
        }
    }
}
