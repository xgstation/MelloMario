using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MelloMario.Factories;

namespace MelloMario.Scripts
{
    class PausedScript : IScript
    {
        public PausedScript()
        {
        }

        public void Bind(IEnumerable<IController> controllers, IGameModel model, IGameControl control)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                // game control commands
                controller.AddCommand(Keys.Q, factory.CreateModelCommand("Quit", model), KeyBehavior.press);
                controller.AddCommand(Keys.R, factory.CreateModelCommand("Reset", model), KeyBehavior.press);
                controller.AddCommand(Keys.P, factory.CreateModelCommand("Pause", model), KeyBehavior.press);
                controller.AddCommand(Keys.F12, factory.CreateModelCommand("ToggleFullScreen", model), KeyBehavior.press);
            }
        }
    }
}
