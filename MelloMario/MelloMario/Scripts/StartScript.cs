using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.Factories;
using Microsoft.Xna.Framework.Input;

namespace MelloMario.Scripts
{
    class StartScript :IScript
    {
        public void Bind(IEnumerable<IController> controllers, IGameModel model, ICharacter character)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                // game character commands
                controller.AddCommand(Keys.A, factory.CreateModelCommand("Normal", model), KeyBehavior.press);
                controller.AddCommand(Keys.B, factory.CreateModelCommand("Infinite", model), KeyBehavior.press);
                controller.AddCommand(Keys.Q, factory.CreateModelCommand("Quit", model), KeyBehavior.press);
            }
        }
    }
}
