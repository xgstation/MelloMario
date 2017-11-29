namespace MelloMario
{
    internal enum KeyBehavior
    {
        press,
        release,
        hold
    }

    internal interface IController
    {
        void AddCommand(object key, ICommand value, KeyBehavior behavior = KeyBehavior.press);
        void Update();
        void Reset();
    }
}
