using MelloMario.MarioObjects;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateGameControlCommand(string action, GameModel model);
        ICommand CreateGameCharacterCommand(string action, IGameCharacter character);
        ICommand CreateGameObjectCommand(string action, IGameObject gameObject);
    }
}
