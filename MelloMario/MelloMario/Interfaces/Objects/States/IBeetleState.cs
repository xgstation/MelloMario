namespace MelloMario
{
    internal interface IBeetleState : IState
    {
        void Show();
        void JumpOn();
        void Pushed();
        void Defeat();
    }
}
