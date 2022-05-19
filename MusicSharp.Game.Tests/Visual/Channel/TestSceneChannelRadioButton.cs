using System.Linq;
using MusicSharp.Game.Overlays.Logging.Channel;
using osu.Framework.Graphics;

namespace MusicSharp.Game.Tests.Visual.Channel
{
    public class TestSceneChannelRadioButton : MusicSharpTestScene
    {
        public TestSceneChannelRadioButton()
        {
            ChannelRadioButtonCollection channelCollection;
            Add(channelCollection = new ChannelRadioButtonCollection
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Items = new[]
                {
                    new RadioButton("log", null),
                    new RadioButton("commands-log", null)
                }
            });
            AddStep("Select log", channelCollection.Items.First().Select);
            AddStep("Select commands-log", channelCollection.Items.Last().Select);
        }
    }
}


