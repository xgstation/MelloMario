namespace MelloMario.Commands
{
    internal class ActionCommand : ICommand
    {
        private GameModel model;

        public ActionCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            //
        }
    }
}