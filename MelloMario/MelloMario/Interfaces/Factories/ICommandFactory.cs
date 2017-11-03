namespace MelloMario
{
    interface ICommandFactory
    {
        ICommand CreateModelCommand(string action, IGameModel model);
        ICommand CreateCharacterCommand(string action, ICharacter character);
    }
}
