using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameCharacter : IGameObject
    {
        Rectangle Viewport { get; }

        void Left();
        void LeftPress();
        void LeftRelease();
        void Right();
        void RightPress();
        void RightRelease();
        void Jump();
        void JumpPress();
        void JumpRelease();
        void Crouch();
        void CrouchPress();
        void CrouchRelease();
        void Action();
    }
}
