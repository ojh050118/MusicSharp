using MusicSharp.Game.Configuration;
using MusicSharp.Game.Graphics;
using MusicSharp.Game.Online;
using MusicSharp.Resources;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osuTK;

namespace MusicSharp.Game
{
    public class MusicSharpGameBase : osu.Framework.Game
    {
        protected override Container<Drawable> Content { get; }

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        protected MusicSharpConfigManager LocalConfig { get; private set; }

        protected Storage Storage { get; set; }

        protected DiscordClient Client { get; set; }

        protected MusicSharpGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768)
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(MusicSharpResources).Assembly));
            var largeStore = new LargeTextureStore(Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, @"Textures")));
            largeStore.AddStore(Host.CreateTextureLoaderStore(new OnlineStore()));

            AddFont(Resources, @"Fonts/OpenSans-Regular");
            AddFont(Resources, @"Fonts/OpenSans-Light");
            AddFont(Resources, @"Fonts/OpenSans-Bold");
            AddFont(Resources, @"Fonts/OpenSans-SemiBold");
            AddFont(Resources, @"Fonts/Noto-Basic");
            AddFont(Resources, @"Fonts/Noto-Hangul");
            AddFont(Resources, @"Fonts/Noto-CJK-Basic");
            AddFont(Resources, @"Fonts/Noto-CJK-Compatibility");

            dependencies.CacheAs(largeStore);
            dependencies.CacheAs(Client = new DiscordClient());
            dependencies.CacheAs(new DiscordColour());
            dependencies.CacheAs(LocalConfig);
            dependencies.CacheAs(this);

            base.Content.Add(Client);
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage ??= host.Storage;
            LocalConfig ??= new MusicSharpConfigManager(Storage);
        }

        protected override void Dispose(bool isDisposing)
        {
            LocalConfig?.Dispose();
            Client?.Stop();

            base.Dispose(isDisposing);
        }
    }
}
