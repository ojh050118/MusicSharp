using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace MusicSharp.Game.Graphics.UserInterface
{
    public class MusicSharpMenuItem : MenuItem
    {
        public readonly MenuItemType Type;
        public readonly IconUsage Icon;

        public MusicSharpMenuItem(string text, IconUsage icon = default, MenuItemType type = MenuItemType.Standard)
            : this(text, type, null)
        {
            Icon = icon;
        }

        public MusicSharpMenuItem(string text, MenuItemType type, Action action, IconUsage icon = default)
            : base(text, action)
        {
            Type = type;
            Icon = icon;
        }
    }
}
