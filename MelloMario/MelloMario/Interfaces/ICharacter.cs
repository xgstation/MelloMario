using Microsoft.Xna.Framework;

namespace MelloMario
{
    internal interface ICharacter
    {
        bool Active { get; }
        IGameWorld CurrentWorld { get; }
        IPlayer Player { get; }
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
        void FireCreate();
        void SuperCreate();
        void NormalCreate();
        void Action();

        void Move(IGameWorld newWorld, Point newLocation);
        void Remove();
    }
}