namespace MelloMario
{
    interface IMarioMovementState : IState
    {
        void Crouch();
        void Idle();
        void Land();
        void Jump();
        void Walk();
    }
}
