namespace MelloMario.Factories
{
    #region

    using System;
    using System.Diagnostics.CodeAnalysis;
    using MelloMario.Controls.Commands;

    #endregion

    internal class CommandFactory : ICommandFactory
    {
        private CommandFactory()
        {
        }

        public static ICommandFactory Instance { get; } = new CommandFactory();

        public ICommand CreateGameCommand(string action, Game1 game)
        {
            switch (action)
            {
                case "CursorUp":
                    return new CursorUp(game);
                case "CursorDown":
                    return new CursorDown(game);
                case "Select":
                    return new Select(game);
                case "Exit":
                    return new Exit(game);
                case "ToggleFullScreen":
                    return new ToggleFullScreen(game);
                case "ToggleMute":
                    return new ToggleMute(game);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ICommand CreateModelCommand(string action, IModel model)
        {
            switch (action)
            {
                case "Pause":
                    return new Pause(model);
                case "Resume":
                    return new Resume(model);
                case "Reset":
                    return new Reset(model);
                case "Exit":
                    return new ExitModel(model);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public ICommand CreateCharacterCommand(string action, ICharacter character)
        {
            switch (action)
            {
                case "Action":
                    return new Controls.Commands.Action(character);
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
                case "BecomeFire":
                    return new FireCreate(character);
                case "BecomeSuper":
                    return new SuperCreate(character);
                case "BecomeNormal":
                    return new NormalCreate(character);
                case "BecomeDead":
                    return new KillMario(character);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }
    }
}
