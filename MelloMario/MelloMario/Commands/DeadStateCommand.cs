namespace MelloMario.Commands
{
    internal class DeadStateCommand : ICommand
    {
        private GameModel model;

        public DeadStateCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Mario.die();
        }
    }
}