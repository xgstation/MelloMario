using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IPlayer : IGameObject, ICharacter
    {
        // TODO: spilt PlayerMario as class MarioCharacter + class Player
        IGameWorld CurrentWorld { get; }
        Rectangle Sensing { get; }
        Rectangle Viewport { get; }

        void Spawn(IGameWorld newWorld);
    }
}
