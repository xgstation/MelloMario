using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IPlayer : IGameObject, ICharacter
    {
        Rectangle Sensing { get; }
        Rectangle Viewport { get; }

        void Spawn(IGameWorld newWorld);
    }
}
