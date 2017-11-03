using MelloMario.Commands;

namespace MelloMario.Factories
{
    class CommandFactory : ICommandFactory
    {
        private static ICommandFactory instance = new CommandFactory();

        private CommandFactory()
        {
        }

        public static ICommandFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public ICommand CreateModelCommand(string action, IGameModel model)
        {
            switch (action)
            {
                case "ToggleFullScreen":
                    return new ToggleFullScreen(model);
                case "Pause":
                    return new Pause(model);
                case "Reset":
                    return new Reset(model);
                case "Quit":
                    return new Quit(model);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public ICommand CreateCharacterCommand(string action, ICharacter character)
        {
            switch (action)
            {
                case "Action":
                    return new Commands.Action(character);
                case "Crouch":
                    return new Crouch(character);
                case "CrouchPress":
                    return new CrouchPress(character);
                case "CrouchRelease":
                    return new CrouchRelease(character);
                case "Jump":
                    return new Jump(character);
                case "JumpPress":
                    return new JumpPress(character);
                case "JumpRelease":
                    return new JumpRelease(character);
                case "Left":
                    return new Left(character);
                case "LeftPress":
                    return new LeftPress(character);
                case "LeftRelease":
                    return new LeftRelease(character);
                case "Right":
                    return new Right(character);
                case "RightPress":
                    return new RightPress(character);
                case "RightRelease":
                    return new RightRelease(character);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }
    }
}
