using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface ICharacter
    {
        bool Active { get; }
        Rectangle Sensing { get; }
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
