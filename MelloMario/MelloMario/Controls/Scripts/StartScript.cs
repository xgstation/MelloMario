namespace MelloMario.Controls.Scripts
{
    using System.Collections.Generic;
    using Factories;
    using Microsoft.Xna.Framework.Input;

    internal class StartScript : IScript
    {
        public void Bind(IEnumerable<IController> controllers, IModel model, ICharacter character)
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
