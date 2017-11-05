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

        public void Bind(IEnumerable<IController> controllers, IGameModel model, ICharacter character)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                controller.AddCommand(Keys.Space, factory.CreateCharacterCommand("Action", character)); // Needs to be implemented
                controller.AddCommand(Keys.Down, factory.CreateCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Down, factory.CreateCharacterCommand("CrouchPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Down, factory.CreateCharacterCommand("CrouchRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Up, factory.CreateCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Up, factory.CreateCharacterCommand("JumpPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Up, factory.CreateCharacterCommand("JumpRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Left, factory.CreateCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Left, factory.CreateCharacterCommand("LeftPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Left, factory.CreateCharacterCommand("LeftRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Right, factory.CreateCharacterCommand("Right", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Right, factory.CreateCharacterCommand("RightPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Right, factory.CreateCharacterCommand("RightRelease", character), KeyBehavior.release);

                controller.AddCommand(Keys.S, factory.CreateCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Keys.S, factory.CreateCharacterCommand("CrouchPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.S, factory.CreateCharacterCommand("CrouchRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Z, factory.CreateCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Z, factory.CreateCharacterCommand("JumpPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Z, factory.CreateCharacterCommand("JumpRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.A, factory.CreateCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Keys.A, factory.CreateCharacterCommand("LeftPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.A, factory.CreateCharacterCommand("LeftRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.D, factory.CreateCharacterCommand("Right", character), KeyBehavior.hold);
                controller.AddCommand(Keys.D, factory.CreateCharacterCommand("RightPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.D, factory.CreateCharacterCommand("RightRelease", character), KeyBehavior.release);

                controller.AddCommand(Keys.F12, factory.CreateModelCommand("ToggleFullScreen", model), KeyBehavior.press);
                controller.AddCommand(Keys.P, factory.CreateModelCommand("Pause", model), KeyBehavior.press);
                controller.AddCommand(Keys.R, factory.CreateModelCommand("Reset", model), KeyBehavior.press);
                controller.AddCommand(Keys.Q, factory.CreateModelCommand("Quit", model), KeyBehavior.press);

                controller.AddCommand(Buttons.B, factory.CreateCharacterCommand("Action", character));
                controller.AddCommand(Buttons.DPadDown, factory.CreateCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadDown, factory.CreateCharacterCommand("CrouchPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadDown, factory.CreateCharacterCommand("CrouchRelease", character), KeyBehavior.release);
                controller.AddCommand(Buttons.A, factory.CreateCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.A, factory.CreateCharacterCommand("JumpPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.A, factory.CreateCharacterCommand("JumpRelease", character), KeyBehavior.release);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateCharacterCommand("LeftPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateCharacterCommand("LeftRelease", character), KeyBehavior.release);
                controller.AddCommand(Buttons.DPadRight, factory.CreateCharacterCommand("Right", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadRight, factory.CreateCharacterCommand("RightPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadRight, factory.CreateCharacterCommand("RightRelease", character), KeyBehavior.release);
            }
        }
    }
}
