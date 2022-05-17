using MusicSharp.Game.Graphics;
using MusicSharp.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace MusicSharp.Game.Overlays.Logging.Channel
{
    public class ChannelButton : DiscordButton
    {
        public string ChannelName
        {
            get => channelName;
            set
            {
                channelName = value;

                if (!IsLoaded)
                    return;

                UpdateContent();
            }
        }

        private string channelName;

        private SpriteText channelText;

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            Colour = colours.DarkGray;
            HoverColour = colours.Gray;
            Content.Padding = new MarginPadding(15);
            Content.Add(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.Distributed)
                },
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.AutoSize)
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new SpriteIcon
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Size = new Vector2(28),
                            Colour = colours.LightestGray,
                            Icon = FontAwesome.Solid.Hashtag
                        },
                        channelText = new SpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            RelativeSizeAxes = Axes.X,
                            Padding = new MarginPadding { Left = 15 },
                            Colour = colours.LightestGray,
                            Font = FontUsage.Default.With(family: "OpenSans-Bold", size: 28),
                            Truncate = true,
                        }
                    }
                }
            });
        }

        protected override void UpdateContent()
        {
            base.UpdateContent();

            channelText.Text = channelName;
        }
    }
}
