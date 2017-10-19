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

        public ICommand CreateGameControlCommand(string action, GameModel model)
        {
            switch (action)
            {
                case "Pause":
                    return new Pause(model);
                default:
                    //it should never hit this case, if it does there is an error somewhere
                    //else in the code
                    return null;
            }
        }

        public ICommand CreateGameCharacterCommand(string action, IGameCharacter character)
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

        // For demo only
        // This method and all of the commands will be removed in the final game
        public ICommand CreateGameObjectCommand(string action, IGameObject gameObject)
        {
            if (gameObject is Mario mario)
            {
                switch (action)
                {
                    case "ToggleGravity":
                        return new ToggleGravity(mario);
                    case "FireState":
                        return new FireState(mario);
                    case "StandardState":
                        return new StandardState(mario);
                    case "SuperState":
                        return new SuperState(mario);
                    default:
                        //it should never hit this case, if it does there is an error somewhere
                        //else in the code
                        return null;
                }
            }
            else
            {
                //it should never hit this case, if it does there is an error somewhere
                //else in the code
                return null;
            }
        }
    }
}
