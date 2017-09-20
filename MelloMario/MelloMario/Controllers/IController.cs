
using MelloMario.Commands;

namespace MelloMario
{
    interface IController
    {
        void AddCommand(int key, ICommand value);

        void UpdateInput();
    }
}
