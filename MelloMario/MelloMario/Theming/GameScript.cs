using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MelloMario.Factories;

namespace MelloMario
{
    class GameScript
    {
        public GameScript()
        {
        }

        public void Bind(IEnumerable<IController> controllers, IGameCharacter character)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                //controller.AddCommand(Keys.Escape, factory.CreateGameModelCommand("Pause", objects));
                controller.AddCommand(Keys.Space, factory.CreateGameCharacterCommand("Action", character));
                controller.AddCommand(Keys.Down, factory.CreateGameCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Up, factory.CreateGameCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Left, factory.CreateGameCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Right, factory.CreateGameCharacterCommand("Right", character), KeyBehavior.hold);
                controller.AddCommand(Keys.Left, factory.CreateGameCharacterCommand("LeftRelease", character), KeyBehavior.release);
                controller.AddCommand(Keys.Right, factory.CreateGameCharacterCommand("RightRelease", character), KeyBehavior.release);

                controller.AddCommand(Keys.S, factory.CreateGameCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Keys.W, factory.CreateGameCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Keys.A, factory.CreateGameCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Keys.D, factory.CreateGameCharacterCommand("Right", character), KeyBehavior.hold);

                // sprint 2 cheat commands

                controller.AddCommand(Keys.G, factory.CreateGameObjectCommand("ToggleGravity", character));

                controller.AddCommand(Keys.Y, factory.CreateGameObjectCommand("StandardState", character));
                controller.AddCommand(Keys.U, factory.CreateGameObjectCommand("SuperState", character));
                controller.AddCommand(Keys.I, factory.CreateGameObjectCommand("FireState", character));
                controller.AddCommand(Keys.O, factory.CreateGameObjectCommand("DeadState", character));

                controller.AddCommand(Buttons.B, factory.CreateGameCharacterCommand("Action", character));
                controller.AddCommand(Buttons.DPadDown, factory.CreateGameCharacterCommand("Crouch", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.A, factory.CreateGameCharacterCommand("Jump", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadLeft, factory.CreateGameCharacterCommand("Left", character), KeyBehavior.hold);
                controller.AddCommand(Buttons.DPadRight, factory.CreateGameCharacterCommand("Right", character), KeyBehavior.hold);
            }
        }
    }
}
