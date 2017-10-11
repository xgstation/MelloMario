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
                default:
                    throw new Exception("Unknown action");
            }
        }

        public ICommand CreateGameObjectCommand(string action, IGameObject gameObject)
        {
            switch (action)
            {
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
