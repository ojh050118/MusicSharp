using System.Linq;
using MusicSharp.Game.Graphics;
using MusicSharp.Game.Graphics.Containers;
using MusicSharp.Game.Online;
using MusicSharp.Game.Overlays.Logging;
using MusicSharp.Game.Overlays.Logging.Channel;
using MusicSharp.Game.Overlays.Logging.Extensions;
using MusicSharp.Game.Users.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace MusicSharp.Game.Overlays.Profile
{
    public class ClientInfo : Container
    {
        private MusicSharpScrollContainer scrollContainer;

        public ChannelRadioButtonCollection ChannelCollection;

        public ChannelRadioButton Current => ChannelCollection.CurrentlySelected;

        private ChannelRadioButton logChannel;
        private ChannelRadioButton commandChannel;

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours, DiscordClient client)
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours.DarkGray
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        scrollContainer = new MusicSharpScrollContainer
                        {
                            RelativeSizeAxes = Axes.Both,
                            Padding = new MarginPadding { Vertical = 80 },
                            Child = new Container
                            {
                                RelativeSizeAxes = Axes.X,
                                AutoSizeAxes = Axes.Y,
                                Padding = new MarginPadding(10),
                                Child = new FillFlowContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Children = new Drawable[]
                                    {
                                        ChannelCollection = new ChannelRadioButtonCollection
                                        {
                                            RelativeSizeAxes = Axes.X,
                                        }
                                    }
                                }
                            }
                        },
                        new UserButton
                        {
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.X,
                            Height = 80
                        },
                        new SettingsToolboxGroup
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 80,
                            Header = "MusicSharp"
                        }
                    }
                }
            };

            ChannelCollection.Items = new[]
            {
                logChannel = new ChannelRadioButton("log")
                {
                    Description = "Discord client log channel."
                },
                commandChannel = new ChannelRadioButton("commands-log")
                {
                    Description = "Command usage history channel."
                }
            };

            client.OnClientLogReceived += log => logChannel.Log?.Invoke(log.ToLogMessage());
            client.OnCommandExecuted += command =>
            {
                var log = new LogMessage(command.User.Username, $"Used {command.User.Username}#{command.User.Discriminator} /{command.CommandName}")
                {
                    User = command.User
                };
                commandChannel.Log?.Invoke(log);
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            setSelectionChannel();
        }

        private void setSelectionChannel()
        {
            ChannelCollection.Items.First().Select();
        }

        protected override bool OnHover(HoverEvent e)
        {
            scrollContainer.ScrollbarVisible = true;

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            scrollContainer.ScrollbarVisible = false;
        }
    }
}
