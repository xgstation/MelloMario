using System.Collections.Generic;
using MelloMario.Factories;
using Microsoft.Xna.Framework.Input;

namespace MelloMario.Scripts
{
    internal class TransistScript : IScript
    {
        public void Bind(IEnumerable<IController> controllers, IGameModel model, ICharacter character)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                // game character commands
                controller.AddCommand(Keys.F12, factory.CreateModelCommand("ToggleFullScreen", model),
                    KeyBehavior.press);
                controller.AddCommand(Keys.R, factory.CreateModelCommand("Reset", model), KeyBehavior.press);
                controller.AddCommand(Keys.Q, factory.CreateModelCommand("Quit", model), KeyBehavior.press);
            }
        }
    }
}