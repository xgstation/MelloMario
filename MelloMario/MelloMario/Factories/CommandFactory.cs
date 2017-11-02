using System;
using MelloMario.Commands;
using MelloMario.MarioObjects;

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
                    Console.WriteLine("Reached");
                    return new Quit(model);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public ICommand CreateControlCommand(string action, IGameControl control)
        {
            switch (action)
            {
                case "Action":
                    return new Commands.Action(control);
                case "Crouch":
                    return new Crouch(control);
                case "CrouchPress":
                    return new CrouchPress(control);
                case "CrouchRelease":
                    return new CrouchRelease(control);
                case "Jump":
                    return new Jump(control);
                case "JumpPress":
                    return new JumpPress(control);
                case "JumpRelease":
                    return new JumpRelease(control);
                case "Left":
                    return new Left(control);
                case "LeftPress":
                    return new LeftPress(control);
                case "LeftRelease":
                    return new LeftRelease(control);
                case "Right":
                    return new Right(control);
                case "RightPress":
                    return new RightPress(control);
                case "RightRelease":
                    return new RightRelease(control);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }
    }
}
