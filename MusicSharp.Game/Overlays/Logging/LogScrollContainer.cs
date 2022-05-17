using System;
using MusicSharp.Game.Graphics.Containers;
using MusicSharp.Game.Online;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace MusicSharp.Game.Overlays.Logging
{
    public class LogScrollContainer : MusicSharpScrollContainer
    {
        private FillFlowContainer messageContent;

        private LogLineContainer lastLog;

        [BackgroundDependencyLoader]
        private void load(DiscordClient client)
        {
            Child = messageContent = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Padding = new MarginPadding { Vertical = 16, Right = 16 },
            };
            client.OnClientLogReceived += addLog;
        }

        private void addLog(Discord.LogMessage log)
        {
            Schedule(() =>
            {
                if (lastLog == null || (DateTime.Now - lastLog.Message.CreatedTime).Seconds > 420)
                    messageContent.Add(lastLog = new LogLineHeader(new LogMessage(log.Message))
                    {
                        Margin = new MarginPadding { Top = lastLog == null ? 0 : 28 }
                    });
                else
                    messageContent.Add(lastLog = new LogLine(new LogMessage(log.Message)));

                if (log.Exception != null)
                    messageContent.Add(new LogLine(new LogMessage(log.Exception.Message)));

                Scheduler.AddDelayed(() => ScrollToEnd(), 100);
            });
        }
    }
}
