namespace MelloMario
{
    interface IMarioMovementState : IState
    {
        void Crouch();
        void Idle();
        void Jump();
        void Walk();
    }
}
