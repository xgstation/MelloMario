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
        void DeleteCommand(object key, KeyBehavior behavior);
        bool ContainsKey(object key, KeyBehavior behavior);
        void Update();
        void Reset();
    }
}
