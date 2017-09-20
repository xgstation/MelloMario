
//mostly a copy of examples and my sprint 0 code.
namespace MelloMario
{
    interface IController
    {
        void AddCommand(int key, ICommand value);

        void UpdateInput();
    }
}
