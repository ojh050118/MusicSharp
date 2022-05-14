using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace MusicSharp.Game.Graphics.UserInterface
{
    public class DiscordButton : ClickableContainer
    {
        private Container container;

        private Box background;
        private Box hover;

        public Color4? HoverColour
        {
            get => hoverColour;
            set
            {
                if (value.HasValue)
                    hoverColour = value.Value;
                else
                    hoverColour = Colour;
            }
        }

        private Color4 hoverColour;

        protected new Container Content;

        public new Color4 Colour
        {
            get => colour;
            set
            {
                colour = value;
                background.Colour = value;
            }
        }

        private Color4 colour;

        public bool LightenOnHover;

        public DiscordButton()
        {
            Child = container = new Container
            {
                Masking = true,
                RelativeSizeAxes = Axes.Both,
                CornerRadius = Height / 10,
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    hover = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0
                    },
                    Content = new Container
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                }
            };
            Enabled.BindValueChanged(onEnableChanged, true);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            container.CornerRadius = Height / 10;
            HoverColour = colour;
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (Enabled.Value)
            {
                if (LightenOnHover)
                    hover.FadeTo(0.1f, 100);

                background.FadeColour(hoverColour, 100);
            }

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (Enabled.Value)
            {
                if (LightenOnHover)
                    hover.FadeOut(100);

                background.FadeColour(colour, 100);
            }
        }

        private void onEnableChanged(ValueChangedEvent<bool> e)
        {
            this.FadeTo(e.NewValue ? 1 : 0.5f, 100);
        }
    }
}
