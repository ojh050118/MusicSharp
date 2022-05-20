using System;

namespace MusicSharp.Game.Overlays.Logging
{
    public class LogMessage : IEquatable<LogMessage>
    {
        public DateTime CreatedTime { get; } = DateTime.Now;

        public string Message { get; }

        public string Author { get; }

        public LogMessage(string author, string message)
        {
            Author = author;
            Message = message;
        }

        public bool Equals(LogMessage other) => CreatedTime == other?.CreatedTime && Message == other.Message;

        public override string ToString() => $"[{CreatedTime.ToString("yyyy.MM.dd")} {CreatedTime.ToString("tt h:mm")}] {Author}: {Message}";
    }
}
