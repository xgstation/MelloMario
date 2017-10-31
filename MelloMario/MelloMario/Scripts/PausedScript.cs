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

        public void Bind(IEnumerable<IController> controllers, IGameCharacter character, IGameModel model)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                // game control commands
                controller.AddCommand(Keys.Q, factory.CreateGameControlCommand("Quit", model), KeyBehavior.press);
                controller.AddCommand(Keys.R, factory.CreateGameControlCommand("Reset", model), KeyBehavior.press);
                controller.AddCommand(Keys.P, factory.CreateGameControlCommand("Pause", model), KeyBehavior.press);
                controller.AddCommand(Keys.F12, factory.CreateGameControlCommand("ToggleFullScreen", model), KeyBehavior.press);
            }
        }
    }
}
