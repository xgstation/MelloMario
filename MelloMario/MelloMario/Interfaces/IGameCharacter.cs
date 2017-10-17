using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameCharacter : IGameObject
    {
        void Left();
        void LeftRelease();
        void LeftPress();
        void Right();
        void RightRelease();
        void RightPress();
        void Jump();
        void JumpRelease();
        void JumpPress();
        void Crouch();
        void CrouchRelease();
        void CrouchPress();
        void Action();
    }
}
