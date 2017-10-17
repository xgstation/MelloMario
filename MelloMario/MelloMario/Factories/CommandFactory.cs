using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    return new PauseCommand(model);
                default:
                    throw new Exception("Unknown action");
            }
        }

        public ICommand CreateGameCharacterCommand(string action, IGameCharacter character)
        {
            switch (action)
            {
                case "Action":
                    return new ActionCommand(character);
                case "Crouch":
                    return new CrouchCommand(character);
                case "Jump":
                    return new JumpCommand(character);
                case "Left":
                    return new LeftCommand(character);
                case "Right":
                    return new RightCommand(character);
                case "LeftRelease":
                    return new LeftReleaseCommand(character);
                case "RightRelease":
                    return new RightReleaseCommand(character);
                case "RightPress":
                    return new RightPressCommand(character);
                case "LeftPress":
                    return new LeftPressCommand(character);
                case "JumpRelease":
                    return new JumpPressCommand(character);
                case "CrouchRelease":
                    return new CrouchReleaseCommand(character);
                case "JumpPress":
                    return new JumpPressCommand(character);
                case "CrouchPress":
                    return new CrouchPressCommand(character);
                default:
                    throw new Exception("Unknown action");
            }
        }

        // For demo only
        // This method and all of the commands will be removed in the final game
        public ICommand CreateGameObjectCommand(string action, IGameObject gameObject)
        {
            switch (action)
            {
                case "ToggleGravity":
                    return new ToggleGravityCommand((Mario)gameObject);
                case "DeadState":
                    return new DeadStateCommand((Mario)gameObject);
                case "FireState":
                    return new FireStateCommand((Mario)gameObject);
                case "StandardState":
                    return new StandardStateCommand((Mario)gameObject);
                case "SuperState":
                    return new SuperStateCommand((Mario)gameObject);
                default:
                    throw new Exception("Unknown action");
            }
        }
    }
}
