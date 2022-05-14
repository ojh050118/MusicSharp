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

        public const float AVATAR_SIZE = 100;

        private DiscordClient client { get; set; }

        [BackgroundDependencyLoader]
        private void load(DiscordColour colours, DiscordClient client)
        {
            this.client = client;
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
                    Padding = new MarginPadding(10),
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            AutoSizeAxes = Axes.Both,
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(10),
                            Children = new Drawable[]
                            {
                                new CircularContainer
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Masking = true,
                                    Size = new Vector2(Size.Y - 20 < AVATAR_SIZE && Size.Y - 20 >= 0 ? Size.Y - 20 : AVATAR_SIZE),
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
                                    AutoSizeAxes = Axes.X,
                                    RelativeSizeAxes = Axes.Y,
                                    ColumnDimensions = new[]
                                    {
                                        new Dimension(GridSizeMode.AutoSize),
                                        new Dimension(GridSizeMode.AutoSize)
                                    },
                                    Content = new[]
                                    {
                                        new Drawable[]
                                        {
                                            username = new SpriteText
                                            {
                                                Anchor = Anchor.BottomLeft,
                                                Origin = Anchor.BottomLeft,
                                                Text = client.User?.Username,
                                                Font = FontUsage.Default.With(size: Size.Y - 20 < AVATAR_SIZE && Size.Y - 20 >= 0 ? (Size.Y - 20) / 2.5f : AVATAR_SIZE / 2.5f),
                                                Truncate = true,
                                            }
                                        },
                                        new Drawable[]
                                        {
                                            discriminator = new SpriteText
                                            {
                                                Anchor = Anchor.TopLeft,
                                                Origin = Anchor.TopLeft,
                                                Alpha = 0.5f,
                                                Text = $"#{client.User?.Discriminator}",
                                                Font = FontUsage.Default.With(size: Size.Y - 20 < AVATAR_SIZE && Size.Y - 20 >= 0 ? (Size.Y - 20) / 2.5f : AVATAR_SIZE / 2.5f),
                                                Truncate = true,
                                            }
                                        }
                                    }
                                }
                            }
                        },
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
