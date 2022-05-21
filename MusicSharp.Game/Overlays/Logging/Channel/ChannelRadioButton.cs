using System;

namespace MusicSharp.Game.Overlays.Logging.Channel
{
    public class ChannelRadioButton : RadioButton
    {
        public ChannelRadioButton(string name, Action action = null)
            : base(name, action)
        {
        }

        public Action<LogMessage> Log;
    }
}
