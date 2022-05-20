using System;
using System.Linq;
using MusicSharp.Game.Graphics;
using MusicSharp.Game.Graphics.Containers;
using MusicSharp.Game.Online;
using MusicSharp.Game.Overlays.Logging.Channel;
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

        public RadioButton Current => ChannelCollection.CurrentlySelected;

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
                new RadioButton("log", Show),
                new RadioButton("commands-log", Show)
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
