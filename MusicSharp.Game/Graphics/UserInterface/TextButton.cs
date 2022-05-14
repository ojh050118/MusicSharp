using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace MusicSharp.Game.Graphics.UserInterface
{
    public class TextButton : DiscordButton
    {
        public string Text
        {
            get => text.Text.ToString();
            set => text.Text = value;
        }

        private SpriteText text;

        public TextButton()
        {
            Content.Add(text = new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Font = FontUsage.Default.With(size: 28),
                Truncate = true,
            });
        }
    }
}
