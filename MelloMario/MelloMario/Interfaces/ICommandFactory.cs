using MelloMario.MarioObjects;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateGameControlCommand(string action, GameModel model);
        ICommand CreateMiscCommand(string action, IGameObject[,] gameObject);
        ICommand CreateMarioCommand(string action, Mario mario);
    }
}
