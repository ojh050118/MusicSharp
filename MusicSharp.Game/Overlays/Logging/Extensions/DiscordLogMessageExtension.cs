namespace MusicSharp.Game.Overlays.Logging.Extensions
{
    public static class DiscordLogMessageExtension
    {
        public static LogMessage ToLogMessage(this Discord.LogMessage log)
        {
            var message = log.Exception == null ? log.Message : log.Exception.Message;

            return new LogMessage(log.Source, message);
        }
    }
}

