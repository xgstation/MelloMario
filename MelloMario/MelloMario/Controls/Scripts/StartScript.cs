namespace MelloMario.Controls.Scripts
{
    #region

    using System.Collections.Generic;
    using MelloMario.Factories;
    using Microsoft.Xna.Framework.Input;

    #endregion

    internal class StartScript : IScript<Game1>
    {
        public void Bind(IEnumerable<IController> controllers, Game1 game)
        {
            ICommandFactory factory = CommandFactory.Instance;

            foreach (IController controller in controllers)
            {
                controller.Reset();

                // game character commands
                controller.AddCommand(Keys.Up, factory.CreateGameCommand("CursorUp", game), KeyBehavior.press);
                controller.AddCommand(Keys.Down, factory.CreateGameCommand("CursorDown", game), KeyBehavior.press);
                controller.AddCommand(Keys.Enter, factory.CreateGameCommand("Select", game), KeyBehavior.press);
                controller.AddCommand(Keys.Q, factory.CreateGameCommand("Exit", game), KeyBehavior.press);
                controller.AddCommand(Keys.F12, factory.CreateGameCommand("ToggleFullScreen", game), KeyBehavior.press);
                controller.AddCommand(Keys.M, factory.CreateGameCommand("ToggleMute", game), KeyBehavior.press);
            }
        }
    }
}
