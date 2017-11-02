using MelloMario.MarioObjects;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateGameControlCommand(string action, IGameModel model);
        ICommand CreateGameCharacterCommand(string action, IGameCharacter character);
    }
}
