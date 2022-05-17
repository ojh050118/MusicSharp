using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace MusicSharp.Game.Graphics.UserInterface
{
    public class TextButton : DiscordButton
    {
        public string Text
        {
            get => text;
            set
            {
                text = value;

                if (!IsLoaded)
                    return;

                UpdateContent();
            }
        }

        private string text;

        private SpriteText buttonText;

        [BackgroundDependencyLoader]
        private void load()
        {
            Content.Add(buttonText = new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Font = FontUsage.Default.With(size: 28),
                Truncate = true,
            });
        }

        protected override void UpdateContent()
        {
            base.UpdateContent();

            buttonText.Text = text;
        }
    }
}
