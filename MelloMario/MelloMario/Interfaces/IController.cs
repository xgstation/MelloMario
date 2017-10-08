namespace MelloMario
{
    interface IController
    {
        void AddCommand(object key, ICommand value);
        void AddHoldCommand(object key, ICommand value);
        void Update();
    }
}
