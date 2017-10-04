
using MelloMario.Commands;

namespace MelloMario
{
    public interface IController
    {
        void AddCommand(object key, ICommand value);
        void Update();
    }
}
