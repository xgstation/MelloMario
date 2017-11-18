namespace MelloMario
{
    internal interface IKoopaState : IState
    {
        void Show();
        void JumpOn();
        void Pushed();
        void Defeat();
    }
}