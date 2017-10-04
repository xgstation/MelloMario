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

        public ICommand CreateMiscCommand(string action, IGameObject[,] gameObject)
        {
            switch (action)
            {
                case "BrickBlock":
                    return new BrickBlockCommand(gameObject);
                case "HiddenBlock":
                    return new HiddenBlockCommand(gameObject);
                case "QuestionBlock":
                    return new QuestionBlockCommand(gameObject);
                case "UsedBlock":
                    return new UsedBlockCommand(gameObject);
                default:
                    throw new Exception("Unknown action");
            }
        }

        public ICommand CreateMarioCommand(string action, Mario mario)
        {
            switch (action)
            {
                case "Action":
                    return new ActionCommand(mario);
                case "Crouch":
                    return new CrouchCommand(mario);
                case "DeadState":
                    return new DeadStateCommand(mario);
                case "FireState":
                    return new FireStateCommand(mario);
                case "Jump":
                    return new JumpCommand(mario);
                case "Left":
                    return new LeftCommand(mario);
                case "Right":
                    return new RightCommand(mario);
                case "StandardState":
                    return new StandardStateCommand(mario);
                case "SuperState":
                    return new SuperStateCommand(mario);
                default:
                    throw new Exception("Unknown action");
            }
        }
    }
}
