namespace MelloMario.Commands
{
    internal class JumpCommand : ICommand
    {
        private GameModel model;

        public JumpCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Mario.up();
        }
    }
}