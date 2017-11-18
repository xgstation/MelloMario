namespace MelloMario
{
    internal interface IItemState : IState
    {
        void Show();
        void Collect();
    }
}