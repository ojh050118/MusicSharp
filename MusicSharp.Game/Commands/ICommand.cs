using System;
using System.Collections.Generic;

namespace MusicSharp.Game.Commands
{
    public interface ICommand : IEquatable<ICommand>
    {
        string Name { get; }

        string Description { get; }

        bool IsGlobalAppCommand { get; }

        IReadOnlyList<ulong> SpecificGuilds { get; }
    }
}
