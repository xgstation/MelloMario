namespace MelloMario
{
    enum KeyBehavior { press, release, hold };

    interface IController
    {
        void AddCommand(object key, ICommand value, KeyBehavior behavior = KeyBehavior.press);
        void Update();
    }
}
