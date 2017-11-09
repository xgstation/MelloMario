using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IPlayer : IGameObject, ICharacter
    {
        IGameWorld CurrentWorld { get; }
        // TODO: spilt PlayerMario as class MarioCharacter + class Player
        Rectangle Sensing { get; }
        Rectangle Viewport { get; }

        void Spawn(IGameWorld newWorld);
        void Reset();
    }
}
