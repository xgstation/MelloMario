namespace MelloMario
{
    internal interface ICommandFactory
    {
        ICommand CreateGameCommand(string action, Game1 game);
        ICommand CreateModelCommand(string action, IModel model);
        ICommand CreateCharacterCommand(string action, ICharacter character);
    }
}
