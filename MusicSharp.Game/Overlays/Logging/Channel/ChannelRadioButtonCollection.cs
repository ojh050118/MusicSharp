using System.Collections.Generic;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace MusicSharp.Game.Overlays.Logging.Channel
{
    public class ChannelRadioButtonCollection : Container
    {
        public IReadOnlyList<RadioButton> Items
        {
            get => items;
            set
            {
                if (ReferenceEquals(items, value))
                    return;

                items = value;

                buttonContainer.Clear();
                items.ForEach(addButton);
            }
        }

        private IReadOnlyList<RadioButton> items;

        private FillFlowContainer<ChannelRadioButton> buttonContainer;

        private RadioButton currentlySelected;

        public ChannelRadioButtonCollection()
        {
            AutoSizeAxes = Axes.Y;
            InternalChild = buttonContainer = new FillFlowContainer<ChannelRadioButton>
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(2.5f),
            };
        }

        private void addButton(RadioButton button)
        {
            button.Selected.ValueChanged += selected =>
            {
                if (selected.NewValue)
                {
                    currentlySelected?.Deselect();
                    currentlySelected = button;
                }
                else
                    currentlySelected = null;
            };

            buttonContainer.Add(new ChannelRadioButton(button));
        }
    }
}
