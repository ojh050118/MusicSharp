using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace MusicSharp.Game.Configuration
{
    public class MusicSharpConfigManager : IniConfigManager<MusicSharpSetting>
    {
        protected override string Filename => @"MusicSharp.ini";

        public MusicSharpConfigManager(Storage storage, IDictionary<MusicSharpSetting, object> defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }

        protected override void InitialiseDefaults()
        {
            SetDefault(MusicSharpSetting.ApplicationID, string.Empty);
            SetDefault(MusicSharpSetting.Token, string.Empty);
            SetDefault(MusicSharpSetting.Prefix, "!");
        }
    }

    public enum MusicSharpSetting
    {
        ApplicationID,
        Token,
        Prefix
    }
}
