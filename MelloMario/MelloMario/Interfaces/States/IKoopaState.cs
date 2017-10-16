namespace MelloMario
{
    interface IKoopaState : IState
    {
        void Show();
        void JumpOn();
        void Defeat();
    }
}
