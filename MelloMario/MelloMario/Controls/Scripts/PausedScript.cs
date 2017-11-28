namespace MelloMario.Controls.Scripts
{
    #region

    using System.Collections.Generic;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework.Input;

    #endregion

    internal class PausedScript : IScript<IModel>
    {
        public void Bind(IEnumerable<IController> controllers, IModel model)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                controller.AddCommand(
                    Keys.F12,
                    factory.CreateModelCommand("ToggleFullScreen", model),
                    KeyBehavior.press);
                controller.AddCommand(Keys.P, factory.CreateModelCommand("Resume", model), KeyBehavior.press);
                controller.AddCommand(Keys.R, factory.CreateModelCommand("Reset", model), KeyBehavior.press);
                controller.AddCommand(Keys.Q, factory.CreateModelCommand("Exit", model), KeyBehavior.press);
            }
        }
    }
}
