using System.Collections.Generic;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace MusicSharp.Game.Overlays.Logging.Channel
{
    public class ChannelRadioButtonCollection : Container
    {
        public IReadOnlyList<ChannelRadioButton> Items
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

        private IReadOnlyList<ChannelRadioButton> items;

        public Bindable<ChannelRadioButton> OnChanged { get; private set; }

        private readonly FillFlowContainer<DrawableChannelRadioButton> buttonContainer;

        public ChannelRadioButton CurrentlySelected { get; private set; }

        public ChannelRadioButtonCollection()
        {
            OnChanged = new Bindable<ChannelRadioButton>();
            AutoSizeAxes = Axes.Y;
            InternalChild = buttonContainer = new FillFlowContainer<DrawableChannelRadioButton>
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(2.5f),
            };
        }

        private void addButton(ChannelRadioButton button)
        {
            button.Selected.ValueChanged += selected =>
            {
                if (selected.NewValue)
                {
                    CurrentlySelected?.Deselect();
                    CurrentlySelected = button;
                    OnChanged.Value = button;
                }
                else
                {
                    CurrentlySelected = null;
                }
            };

            buttonContainer.Add(new DrawableChannelRadioButton(button));
        }
    }
}
