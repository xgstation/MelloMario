using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameCharacter
    {
        void Left();
        void Right();
        void Jump();
        void Crouch();
        void Action();
    }
}
