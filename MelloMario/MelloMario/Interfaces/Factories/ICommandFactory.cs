namespace MelloMario
{
    internal interface ICommandFactory
    {
        ICommand CreateModelCommand(string action, IModel model);
        ICommand CreateCharacterCommand(string action, ICharacter character);
    }
}
