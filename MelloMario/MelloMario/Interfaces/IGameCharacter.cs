using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameCharacter : IGameObject
    {
        void Left();
        void LeftRelease();
        void RightRelease();
        void Right();
        void Jump();
        void Crouch();
        void Action();
    }
}
