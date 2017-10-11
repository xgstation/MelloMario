using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MelloMario.Controllers;
using MelloMario.MarioObjects;
using MelloMario.Factories;

namespace MelloMario
{
    class GameScript
    {
        public GameScript()
        {
        }

        public void Bind(IEnumerable<IController> controllers, Mario mario)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                //controller.AddCommand(Keys.Escape, factory.CreateGameModelCommand("Pause", objects));
                //controller.AddCommand(Keys.Down, factory.CreateGameCharacterCommand("Fall", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Space, factory.CreateGameCharacterCommand("Action", mario));
                controller.AddCommand(Keys.Down, factory.CreateGameCharacterCommand("Crouch", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Up, factory.CreateGameCharacterCommand("Jump", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Left, factory.CreateGameCharacterCommand("Left", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Right, factory.CreateGameCharacterCommand("Right", mario), KeyBehavior.hold);

                controller.AddCommand(Keys.S, factory.CreateGameCharacterCommand("Crouch", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.W, factory.CreateGameCharacterCommand("Jump", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.A, factory.CreateGameCharacterCommand("Left", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.D, factory.CreateGameCharacterCommand("Right", mario), KeyBehavior.hold);

                controller.AddCommand(Keys.Y, factory.CreateGameObjectCommand("StandardState", mario));
                controller.AddCommand(Keys.U, factory.CreateGameObjectCommand("SuperState", mario));
                controller.AddCommand(Keys.I, factory.CreateGameObjectCommand("FireState", mario));
                controller.AddCommand(Keys.O, factory.CreateGameObjectCommand("DeadState", mario));

                controller.AddCommand(Buttons.B, factory.CreateGameCharacterCommand("Action", mario));
                controller.AddCommand(Buttons.DPadDown, factory.CreateGameCharacterCommand("Crouch", mario), KeyBehavior.hold);
                controller.AddCommand(Buttons.A, factory.CreateGameCharacterCommand("Jump", mario), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateGameCharacterCommand("Left", mario), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadRight, factory.CreateGameCharacterCommand("Right", mario), KeyBehavior.hold);
            }
        }
    }
}
