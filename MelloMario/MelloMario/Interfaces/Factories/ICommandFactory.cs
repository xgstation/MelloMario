using MelloMario.MarioObjects;

namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateGameControlCommand(string action, GameModel model);
        ICommand CreateGameCharacterCommand(string action, IGameCharacter character, IMarioMovementState move);
        ICommand CreateGameObjectCommand(string action, IGameObject gameObject);
    }
}
