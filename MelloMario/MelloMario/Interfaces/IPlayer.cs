using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IPlayer
    {
        IGameWorld World { get; }
        ICharacter Character { get; }
        Rectangle Sensing { get; }
        Rectangle Viewport { get; }

        void Spawn(IGameWorld newWorld);
        void Reset();
    }
}
