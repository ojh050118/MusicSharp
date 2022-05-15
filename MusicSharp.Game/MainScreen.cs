using MusicSharp.Game.Graphics;
using MusicSharp.Game.Graphics.UserInterface;
using MusicSharp.Game.Online;
using MusicSharp.Game.Overlays.Logging;
using MusicSharp.Game.Overlays.Logging.Channel;
using osu.Framework.Allocation;
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

        [BackgroundDependencyLoader]
        private void load(DiscordColour colour, DiscordClient client)
        {
            InternalChildren = new Drawable[]
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
                        new Dimension(GridSizeMode.AutoSize),
                        new Dimension(GridSizeMode.Distributed),
                        new Dimension(GridSizeMode.AutoSize),
                    },
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            new ChannelHeader
                            {
                                RelativeSizeAxes = Axes.X,
                                Height = 80,
                                Header = "Log",
                                Description = "This is a discord client logging channel."
                            }
                        },
                        new Drawable[]
                        {
                            new LogScrollContainer
                            {
                                RelativeSizeAxes = Axes.Both
                            },
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

            };

            this.client = client;
            client.IsRunning.ValueChanged += v =>
            {
                stopButton.Enabled.Value = v.NewValue;
                startButton.Enabled.Value = !v.NewValue;
            };
            client.IsRunning.TriggerChange();
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
