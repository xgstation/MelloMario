using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MelloMario.Factories;

namespace MelloMario
{
    class GameScript
    {
        private ICommandFactory factory;
        public GameScript()
        {
        }

        public void Bind(IEnumerable<IController> controllers, IGameCharacter character, GameModel model)
        {
            factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                //controller.AddCommand(Keys.Escape, factory.CreateGameModelCommand("Pause", objects));
                controller.AddCommand(Keys.Space, factory.CreateGameCharacterCommand("Action", character)); // Needs to be implemented
                controller.AddCommand(Keys.Down, factory.CreateGameCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Down, factory.CreateGameCharacterCommand("CrouchPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Down, factory.CreateGameCharacterCommand("CrouchRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Up, factory.CreateGameCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Up, factory.CreateGameCharacterCommand("JumpPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Up, factory.CreateGameCharacterCommand("JumpRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Left, factory.CreateGameCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Left, factory.CreateGameCharacterCommand("LeftPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Left, factory.CreateGameCharacterCommand("LeftRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Right, factory.CreateGameCharacterCommand("Right", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Right, factory.CreateGameCharacterCommand("RightPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Right, factory.CreateGameCharacterCommand("RightRelease", character), KeyBehavior.release);

                controller.AddCommand(Keys.S, factory.CreateGameCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Keys.S, factory.CreateGameCharacterCommand("CrouchPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.S, factory.CreateGameCharacterCommand("CrouchRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Z, factory.CreateGameCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Z, factory.CreateGameCharacterCommand("JumpPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.Z, factory.CreateGameCharacterCommand("JumpRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.A, factory.CreateGameCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Keys.A, factory.CreateGameCharacterCommand("LeftPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.A, factory.CreateGameCharacterCommand("LeftRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.D, factory.CreateGameCharacterCommand("Right", character), KeyBehavior.hold);
                controller.AddCommand(Keys.D, factory.CreateGameCharacterCommand("RightPress", character), KeyBehavior.press);
                controller.AddCommand(Keys.D, factory.CreateGameCharacterCommand("RightRelease", character), KeyBehavior.release);

                // Need to quit game using Q
                controller.AddCommand(Keys.Q, factory.CreateGameControlCommand("Quit", model), KeyBehavior.press);

                // sprint 2 cheat commands
                controller.AddCommand(Keys.Y, factory.CreateGameObjectCommand("StandardState", character));
                controller.AddCommand(Keys.U, factory.CreateGameObjectCommand("SuperState", character));
                controller.AddCommand(Keys.I, factory.CreateGameObjectCommand("FireState", character));

                controller.AddCommand(Buttons.B, factory.CreateGameCharacterCommand("Action", character));
                controller.AddCommand(Buttons.DPadDown, factory.CreateGameCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadDown, factory.CreateGameCharacterCommand("CrouchPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadDown, factory.CreateGameCharacterCommand("CrouchRelease", character), KeyBehavior.release);
                controller.AddCommand(Buttons.A, factory.CreateGameCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.A, factory.CreateGameCharacterCommand("JumpPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.A, factory.CreateGameCharacterCommand("JumpRelease", character), KeyBehavior.release);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateGameCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateGameCharacterCommand("LeftPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateGameCharacterCommand("LeftRelease", character), KeyBehavior.release);
                controller.AddCommand(Buttons.DPadRight, factory.CreateGameCharacterCommand("Right", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadRight, factory.CreateGameCharacterCommand("RightPress", character), KeyBehavior.press);
                controller.AddCommand(Buttons.DPadRight, factory.CreateGameCharacterCommand("RightRelease", character), KeyBehavior.release);

            }
        }
    }
}
