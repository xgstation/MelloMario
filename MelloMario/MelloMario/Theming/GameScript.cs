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

        public void Bind(IList<IController> controllers, Mario mario, List<IGameObject>[,] allObjects)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                if (controller is KeyboardController)
                {
                    //controller.AddCommand(Keys.Escape, factory.CreateGameModelCommand("Pause", objects));
                    //controller.AddCommand(Keys.A, factory.CreateMarioCommand("Jump", mario));
                    controller.AddCommand(Keys.Down, factory.CreateMarioCommand("Crouch", mario));
                    controller.AddCommand(Keys.Up, factory.CreateMarioCommand("Jump", mario));
                    controller.AddHoldCommand(Keys.Left, factory.CreateMarioCommand("Left", mario));
                    controller.AddHoldCommand(Keys.Right, factory.CreateMarioCommand("Right", mario));
                    controller.AddCommand(Keys.Space, factory.CreateMarioCommand("Action", mario));

                    controller.AddCommand(Keys.S, factory.CreateMarioCommand("Crouch", mario));
                   controller.AddCommand(Keys.W, factory.CreateMarioCommand("Jump", mario));
                    controller.AddHoldCommand(Keys.A, factory.CreateMarioCommand("Left", mario));
                    controller.AddHoldCommand(Keys.D, factory.CreateMarioCommand("Right", mario));
                    controller.AddVerticalCommand(Keys.W, factory.CreateMarioCommand("Jump", mario));
                    controller.AddVerticalCommand(Keys.S, factory.CreateMarioCommand("Fall", mario));

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
                }
                else
                if (controller is GamepadController)
                {
                    // controller.AddCommand(Buttons.Start, factory.CreateGameModelCommand("Pause", model));
                    controller.AddCommand(Buttons.A, factory.CreateMarioCommand("Jump", mario));
                    controller.AddCommand(Buttons.DPadDown, factory.CreateMarioCommand("Crouch", mario));
                    controller.AddHoldCommand(Buttons.DPadLeft, factory.CreateMarioCommand("Left", mario));
                    controller.AddHoldCommand(Buttons.DPadRight, factory.CreateMarioCommand("Right", mario));
                    controller.AddCommand(Buttons.B, factory.CreateMarioCommand("Action", mario));
                }
            }
        }
    }
}
