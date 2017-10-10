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
                //controller.AddCommand(Keys.A, factory.CreateMarioCommand("Jump", mario));
                controller.AddCommand(Keys.Down, factory.CreateMarioCommand("Fall", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Up, factory.CreateMarioCommand("Jump", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Left, factory.CreateMarioCommand("Left", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Right, factory.CreateMarioCommand("Right", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.Space, factory.CreateMarioCommand("Action", mario));

                controller.AddCommand(Keys.S, factory.CreateMarioCommand("Fall", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.W, factory.CreateMarioCommand("Jump", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.A, factory.CreateMarioCommand("Left", mario), KeyBehavior.hold);
                controller.AddCommand(Keys.D, factory.CreateMarioCommand("Right", mario), KeyBehavior.hold);

                //commands for changing block/mario state
                //comented out because outdated and not needed after sprint 1
                /*
                controller.AddCommand(Keys.X, factory.CreateMiscCommand("UsedBlock", allObjects));
                controller.AddCommand(Keys.OemQuestion, factory.CreateMiscCommand("QuestionBlock", allObjects));
                controller.AddCommand(Keys.B, factory.CreateMiscCommand("BrickBlock", staticBlocks));
                controller.AddCommand(Keys.H, factory.CreateMiscCommand("HiddenBlock", staticBlocks));
                */

                controller.AddCommand(Keys.Y, factory.CreateMarioCommand("StandardState", mario));
                controller.AddCommand(Keys.U, factory.CreateMarioCommand("SuperState", mario));
                controller.AddCommand(Keys.I, factory.CreateMarioCommand("FireState", mario));
                controller.AddCommand(Keys.O, factory.CreateMarioCommand("DeadState", mario));

                // controller.AddCommand(Buttons.Start, factory.CreateGameModelCommand("Pause", model));
                controller.AddCommand(Buttons.A, factory.CreateMarioCommand("Jump", mario), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadDown, factory.CreateMarioCommand("Crouch", mario), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateMarioCommand("Left", mario), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadRight, factory.CreateMarioCommand("Right", mario), KeyBehavior.hold);
                controller.AddCommand(Buttons.B, factory.CreateMarioCommand("Action", mario));
            }
        }
    }
}
