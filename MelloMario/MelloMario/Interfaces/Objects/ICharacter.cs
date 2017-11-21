namespace MelloMario
{
    #region

    using Microsoft.Xna.Framework;

    #endregion

    internal interface ICharacter
    {
        bool Active { get; }
        IWorld CurrentWorld { get; }
        IPlayer Player { get; }
        Rectangle Sensing { get; }

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

        void Move(IWorld newWorld, Point newLocation);
        void Remove();
    }
}
