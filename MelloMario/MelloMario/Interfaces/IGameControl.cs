using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameControl
    {
        Rectangle Viewport { get; }

        void Spawn(IGameWorld newWorld);
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
