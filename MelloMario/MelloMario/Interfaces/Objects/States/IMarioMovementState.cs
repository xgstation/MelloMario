namespace MelloMario
{
    interface IMarioMovementState : IState
    {
        void Crouch();
        void Idle();
        void Landing();
        void Jump();
        void Walk();
    }
}
