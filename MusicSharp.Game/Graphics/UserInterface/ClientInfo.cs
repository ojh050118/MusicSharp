using MusicSharp.Game.Online;
using MusicSharp.Game.Users.Drawables;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace MusicSharp.Game.Graphics.UserInterface
{
    public class ClientInfo : Container
    {
        private DrawableAvatar avatar;
        private SpriteText username;
        private SpriteText discriminator;

        private Container content;

        public const float AVATAR_SIZE = 80;
        public const float PADDING = 15;

        private DiscordClient client { get; set; }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours, DiscordClient client)
        {
            this.client = client;
            var avatarSize = Size.Y - PADDING * 2 < AVATAR_SIZE && Size.Y - PADDING * 2 >= 0 ? Size.Y - PADDING * 2 : AVATAR_SIZE;
            var fontSize = Size.Y - PADDING * 2 < AVATAR_SIZE && Size.Y - PADDING * 2 >= 0 ? (Size.Y - PADDING * 2) / 2.5f : AVATAR_SIZE / 2.5f;
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours.DarkGray
                },
                content = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = colours.DarkerGray
                                },
                                new Container
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Padding = new MarginPadding(PADDING),
                                    Child = new GridContainer
                                    {
                                        RelativeSizeAxes = Axes.X,
                                        AutoSizeAxes = Axes.Y,
                                        Anchor = Anchor.BottomLeft,
                                        Origin = Anchor.BottomLeft,
                                        RowDimensions = new[]
                                        {
                                            new Dimension(GridSizeMode.AutoSize, maxSize: avatarSize + PADDING),
                                            new Dimension(GridSizeMode.Relative),
                                        },
                                        ColumnDimensions = new[]
                                        {
                                            new Dimension(GridSizeMode.AutoSize, minSize: avatarSize + PADDING),
                                        },
                                        Content = new[]
                                        {
                                            new Drawable[]
                                            {
                                                new CircularContainer
                                                {
                                                    Anchor = Anchor.CentreLeft,
                                                    Origin = Anchor.CentreLeft,
                                                    Masking = true,
                                                    Size = new Vector2(avatarSize),
                                                    Child = new DelayedLoadWrapper(avatar = new DrawableAvatar(client.User)
                                                    {
                                                        RelativeSizeAxes = Axes.Both
                                                    })
                                                    {
                                                        RelativeSizeAxes = Axes.Both
                                                    }
                                                },
                                                new GridContainer
                                                {
                                                    RelativeSizeAxes = Axes.Both,
                                                    ColumnDimensions = new[]
                                                    {
                                                        new Dimension(GridSizeMode.Distributed)
                                                    },
                                                    Content = new[]
                                                    {
                                                        new Drawable[]
                                                        {
                                                            username = new SpriteText
                                                            {
                                                                Anchor = Anchor.BottomLeft,
                                                                Origin = Anchor.BottomLeft,
                                                                RelativeSizeAxes = Axes.X,
                                                                Text = client.User?.Username,
                                                                Font = FontUsage.Default.With(size: fontSize),
                                                                Truncate = true,
                                                            }
                                                        },
                                                        new Drawable[]
                                                        {
                                                            discriminator = new SpriteText
                                                            {
                                                                Anchor = Anchor.TopLeft,
                                                                Origin = Anchor.TopLeft,
                                                                RelativeSizeAxes = Axes.X,
                                                                Alpha = 0.5f,
                                                                Text = $"#{client.User?.Discriminator}",
                                                                Font = FontUsage.Default.With(size: fontSize),
                                                                Truncate = true,
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }
                    }
                }
            };

            client.OnClientReady += refresh;
            client.OnClientLoggedOut += refresh;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            content.FadeInFromZero(300, Easing.OutQuint);
        }

        private void refresh()
        {
            Schedule(() => content.Alpha = 0);
            username.Text = client.User?.Username;
            discriminator.Text = $"#{client.User?.Discriminator}";
            avatar.UpdateAvatar(client.User?.GetAvatarUrl());

            Schedule(() => content.FadeIn(300, Easing.OutQuint));
        }
    }
}
