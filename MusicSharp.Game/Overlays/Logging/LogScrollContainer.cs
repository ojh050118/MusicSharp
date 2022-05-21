using System;
using MusicSharp.Game.Graphics.Containers;
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
        private void load()
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
            if (lastLog == null || (DateTime.Now - lastLog.Message.CreatedTime).Seconds > 420 || lastLog.Message.Author != log.Author)
            {
                messageContent.Add(lastLog = new LogLineHeader(log)
                {
                    Margin = new MarginPadding { Top = lastLog == null ? 0 : 24 }
                });
            }
            else
                messageContent.Add(new LogLine(log));

            Scheduler.AddDelayed(() => ScrollToEnd(), 100);
        }
    }
}
