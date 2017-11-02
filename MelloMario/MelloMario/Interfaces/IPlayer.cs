using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IPlayer : IGameObject, ICharacter
    {
        Rectangle Viewport { get; }

        void Spawn(IGameWorld newWorld);
    }
}
