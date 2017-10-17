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
                    return new Pause(model);
                default:
                    throw new Exception("Unknown action");
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
                    return new ToggleGravity((Mario)gameObject);
                case "DeadState":
                    return new DeadState((Mario)gameObject);
                case "FireState":
                    return new FireState((Mario)gameObject);
                case "StandardState":
                    return new StandardState((Mario)gameObject);
                case "SuperState":
                    return new SuperState((Mario)gameObject);
                default:
                    throw new Exception("Unknown action");
            }
        }
    }
}
