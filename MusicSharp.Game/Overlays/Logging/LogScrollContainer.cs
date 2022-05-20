using System;
using MusicSharp.Game.Graphics.Containers;
using MusicSharp.Game.Online;
using MusicSharp.Game.Overlays.Logging.Extensions;
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
        }

        public void AddLog(LogMessage log)
        {
            if (lastLog == null || (DateTime.Now - lastLog.Message.CreatedTime).Seconds > 420)
            {
                messageContent.Add(lastLog = new LogLineHeader(log)
                {
                    Margin = new MarginPadding { Top = lastLog == null ? 0 : 28 }
                });
            }
            else
                messageContent.Add(new LogLine(log));

            Scheduler.AddDelayed(() => ScrollToEnd(), 100);
        }
    }
}
