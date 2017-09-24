using MelloMario.Commands;

namespace MelloMario
{
    internal class LeftCommand : ICommand
    {
        private GameModel model;

        public LeftCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            this.model.Mario.left();
        }
    }
}