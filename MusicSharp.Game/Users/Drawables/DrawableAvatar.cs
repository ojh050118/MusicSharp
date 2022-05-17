using System.IO;
using Discord.WebSocket;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Utils;

namespace MusicSharp.Game.Users.Drawables
{
    [LongRunningLoad]
    public class DrawableAvatar : Sprite
    {
        private SocketUser user;

        public const string AVATAR_URL = @"https://cdn.discordapp.com/embed/avatars/";

        [Resolved]
        private LargeTextureStore textures { get; set; }

        public DrawableAvatar(SocketUser user)
        {
            this.user = user;

            RelativeSizeAxes = Axes.Both;
            FillMode = FillMode.Fit;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures)
        {
            if (user != null)
                Texture = textures.Get(user.GetAvatarUrl());

            Texture ??= textures.Get(Path.Combine(AVATAR_URL, $"{RNG.Next(1, 6)}.png"));
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            this.FadeInFromZero(200);
        }

        public void UpdateAvatar(string url)
        {
            if (!string.IsNullOrEmpty(url))
                Texture = textures.Get(url);
            else
                Texture = textures.Get(Path.Combine(AVATAR_URL, $"{RNG.Next(1, 6)}.png"));

            Schedule(() => this.FadeInFromZero(200));
        }
    }
}
