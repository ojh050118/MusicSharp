using System;
using osu.Framework.Bindables;

namespace MusicSharp.Game.Overlays.Logging.Channel
{
    public class RadioButton
    {
        public readonly BindableBool Selected;

        public string Label;

        public string Description;

        private readonly Action action;

        public RadioButton(string label, Action action)
        {
            Label = label;
            this.action = action;
            Selected = new BindableBool();
        }

        public void Select()
        {
            if (!Selected.Value)
            {
                Selected.Value = true;
                action?.Invoke();
            }
        }

        public void Deselect() => Selected.Value = false;
    }
}
