using System;
using MusicSharp.Game.Graphics;
using MusicSharp.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace MusicSharp.Game.Overlays.Logging.Channel
{
    public class ChannelRadioButton : DiscordButton
    {
        public Action<RadioButton> Selected;

        public readonly RadioButton Button;

        private Color4 defaultBackgroundColour;
        private Color4 defaultHoverColour;
        private Color4 defaultSelectedColour;

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

        public ChannelRadioButton(RadioButton button)
        {
            Button = button;
            ChannelName = button.Label;
            Action = button.Select;
            RelativeSizeAxes = Axes.X;
            Height = 60;
        }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            Colour = defaultBackgroundColour = colours.DarkGray;
            defaultSelectedColour = colours.LightGray;
            HoverColour = defaultHoverColour = colours.Gray;
            Content.Padding = new MarginPadding(15);
            Content.Add(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension()
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

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Button.Selected.ValueChanged += selected =>
            {
                updateSelectionState();
                if (selected.NewValue)
                    Selected?.Invoke(Button);
            };

            Button.Selected.BindDisabledChanged(disabled => Enabled.Value = !disabled, true);
            updateSelectionState();
        }

        private void updateSelectionState()
        {
            if (!IsLoaded)
                return;

            Colour = Button.Selected.Value ? defaultSelectedColour : defaultBackgroundColour;
            HoverColour = Button.Selected.Value ? defaultSelectedColour : defaultHoverColour;
        }

        protected override void UpdateContent()
        {
            base.UpdateContent();

            channelText.Text = channelName;
        }
    }
}
