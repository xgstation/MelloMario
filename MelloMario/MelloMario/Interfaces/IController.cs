
using MelloMario.Commands;

namespace MelloMario
{
    public interface IController
    {
        void AddCommand(int key, ICommand value);
        void Update();
    }
}
