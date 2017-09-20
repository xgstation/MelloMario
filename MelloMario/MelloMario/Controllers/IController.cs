
//mostly a copy of examples and my sprint 0 code.
using MelloMario.Commands;

namespace MelloMario
{
    interface IController
    {
        void AddCommand(int key, ICommand value);

        void UpdateInput();
    }
}
