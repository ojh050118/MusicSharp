using Discord;
using MusicSharp.Game.Graphics.Containers;
using MusicSharp.Game.Online;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace MusicSharp.Game.Overlays.Logging
{
    public class LogScrollContainer : MusicSharpScrollContainer
    {
        private FillFlowContainer messageContent;

        [BackgroundDependencyLoader]
        private void load(DiscordClient client)
        {
            Child = messageContent = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Padding = new MarginPadding(20)
            };
            client.OnClientLogReceived += addLog;
        }

        private void addLog(LogMessage log)
        {
            Schedule(() =>
            {
                messageContent.Add(new SpriteText
                {
                    RelativeSizeAxes = Axes.X,
                    Text = $"[{log.Source}]: {log.Message}",
                    AllowMultiline = true,
                    Font = FontUsage.Default.With(size: 28)
                });

                if (log.Exception != null)
                {
                    messageContent.Add(new SpriteText
                    {
                        RelativeSizeAxes = Axes.X,
                        Text = $"[Error]: {log.Exception.Message}",
                        AllowMultiline = true,
                        Font = FontUsage.Default.With(size: 28)
                    });
                }

                Scheduler.AddDelayed(() => ScrollToEnd(), 100);
            });
        }
    }
}
