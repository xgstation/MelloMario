using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IPlayer
    {
        IGameWorld World { get; }
        ICharacter Character { get; }

        void Spawn(IGameWorld newWorld, Point newLocation);
        void Reset(ICharacter newCharacter);
    }
}
