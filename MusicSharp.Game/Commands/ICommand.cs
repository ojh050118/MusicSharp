using System;

namespace MusicSharp.Game.Commands
{
    public interface ICommand : IEquatable<ICommand>
    {
        string Name { get; }

        string Description { get; }
    }
}
