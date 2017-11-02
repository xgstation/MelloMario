using MelloMario.MarioObjects;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateModelCommand(string action, IGameModel model);
        ICommand CreateControlCommand(string action, IGameControl control);
    }
}
