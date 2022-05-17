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
            set => hoverColour = value ?? Colour;
        }

        private Color4 hoverColour;

        private Container content;

        protected new Container<Drawable> Content => content;

        public new Color4 Colour
        {
            get => colour;
            set
            {
                colour = value;

                if (!IsLoaded)
                    return;

                UpdateContent();
                background.Colour = value;
            }
        }

        private Color4 colour;

        public bool LightenOnHover;

        [BackgroundDependencyLoader]
        private void load()
        {
            HoverColour = colour;
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
                    content = new Container
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                }
            };
            Enabled.BindValueChanged(onEnableChanged, true);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            UpdateContent();
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

        protected virtual void UpdateContent()
        {
            container.CornerRadius = Height / 10;
            background.Colour = colour;
        }

        private void onEnableChanged(ValueChangedEvent<bool> e)
        {
            this.FadeTo(e.NewValue ? 1 : 0.5f, 100);
        }
    }
}
