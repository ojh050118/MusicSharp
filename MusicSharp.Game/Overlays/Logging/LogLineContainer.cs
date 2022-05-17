using MusicSharp.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace MusicSharp.Game.Overlays.Logging
{
    public abstract class LogLineContainer : Container
    {
        public const float MARGIN = 100;
        public const float PADDING = 20;
        public const int LEFT_CONTENT_SIZE = 72;

        public LogMessage Message
        {
            get => message;
            set
            {
                message = value;

                if (!IsLoaded)
                    return;

                UpdateMessageContent();
            }
        }

        private LogMessage message;

        private Box background;

        [Resolved]
        private DiscordColour colours { get; set; }

        private Container content;

        protected new Container<Drawable> Content => content;

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours)
        {
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours.Gray
                },
                content = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding { Left = PADDING, Right = MARGIN + PADDING }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            UpdateMessageContent();
        }

        protected override bool OnHover(HoverEvent e)
        {
            background.FadeColour(colours.DarkGray);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            background.FadeColour(colours.Gray);
        }

        protected abstract void UpdateMessageContent();
    }
}

