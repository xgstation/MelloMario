namespace MelloMario.Commands
{
    internal class RightCommand : ICommand
    {
        private GameModel model;

        public RightCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Mario.right();
        }
    }
}