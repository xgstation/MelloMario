using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MelloMario.Factories;

namespace MelloMario.Scripts
{
    class PlayingScript : IScript
    {
        public PlayingScript()
        {
        }

        public void Bind(IEnumerable<IController> controllers, IGameModel model, IGameControl control)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                controller.AddCommand(Keys.Space, factory.CreateControlCommand("Action", control)); // Needs to be implemented
                controller.AddCommand(Keys.Down, factory.CreateControlCommand("Crouch", control), KeyBehavior.hold);
                controller.AddCommand(Keys.Down, factory.CreateControlCommand("CrouchPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.Down, factory.CreateControlCommand("CrouchRelease", control), KeyBehavior.release);
                controller.AddCommand(Keys.Up, factory.CreateControlCommand("Jump", control), KeyBehavior.hold);
                controller.AddCommand(Keys.Up, factory.CreateControlCommand("JumpPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.Up, factory.CreateControlCommand("JumpRelease", control), KeyBehavior.release);
                controller.AddCommand(Keys.Left, factory.CreateControlCommand("Left", control), KeyBehavior.hold);
                controller.AddCommand(Keys.Left, factory.CreateControlCommand("LeftPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.Left, factory.CreateControlCommand("LeftRelease", control), KeyBehavior.release);
                controller.AddCommand(Keys.Right, factory.CreateControlCommand("Right", control), KeyBehavior.hold);
                controller.AddCommand(Keys.Right, factory.CreateControlCommand("RightPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.Right, factory.CreateControlCommand("RightRelease", control), KeyBehavior.release);

                controller.AddCommand(Keys.S, factory.CreateControlCommand("Crouch", control), KeyBehavior.hold);
                controller.AddCommand(Keys.S, factory.CreateControlCommand("CrouchPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.S, factory.CreateControlCommand("CrouchRelease", control), KeyBehavior.release);
                controller.AddCommand(Keys.Z, factory.CreateControlCommand("Jump", control), KeyBehavior.hold);
                controller.AddCommand(Keys.Z, factory.CreateControlCommand("JumpPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.Z, factory.CreateControlCommand("JumpRelease", control), KeyBehavior.release);
                controller.AddCommand(Keys.A, factory.CreateControlCommand("Left", control), KeyBehavior.hold);
                controller.AddCommand(Keys.A, factory.CreateControlCommand("LeftPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.A, factory.CreateControlCommand("LeftRelease", control), KeyBehavior.release);
                controller.AddCommand(Keys.D, factory.CreateControlCommand("Right", control), KeyBehavior.hold);
                controller.AddCommand(Keys.D, factory.CreateControlCommand("RightPress", control), KeyBehavior.press);
                controller.AddCommand(Keys.D, factory.CreateControlCommand("RightRelease", control), KeyBehavior.release);

                // game control commands
                controller.AddCommand(Keys.Q, factory.CreateModelCommand("Quit", model), KeyBehavior.press);
                controller.AddCommand(Keys.R, factory.CreateModelCommand("Reset", model), KeyBehavior.press);
                controller.AddCommand(Keys.P, factory.CreateModelCommand("Pause", model), KeyBehavior.press);
                controller.AddCommand(Keys.F12, factory.CreateModelCommand("ToggleFullScreen", model), KeyBehavior.press);

                controller.AddCommand(Buttons.B, factory.CreateControlCommand("Action", control));
                controller.AddCommand(Buttons.DPadDown, factory.CreateControlCommand("Crouch", control), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadDown, factory.CreateControlCommand("CrouchPress", control), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadDown, factory.CreateControlCommand("CrouchRelease", control), KeyBehavior.release);
                controller.AddCommand(Buttons.A, factory.CreateControlCommand("Jump", control), KeyBehavior.hold);
                controller.AddCommand(Buttons.A, factory.CreateControlCommand("JumpPress", control), KeyBehavior.press);
                controller.AddCommand(Buttons.A, factory.CreateControlCommand("JumpRelease", control), KeyBehavior.release);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateControlCommand("Left", control), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateControlCommand("LeftPress", control), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateControlCommand("LeftRelease", control), KeyBehavior.release);
                controller.AddCommand(Buttons.DPadRight, factory.CreateControlCommand("Right", control), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadRight, factory.CreateControlCommand("RightPress", control), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadRight, factory.CreateControlCommand("RightRelease", control), KeyBehavior.release);

            }
        }
    }
}
