namespace MelloMario.Commands
{
    internal class SuperStateCommand : ICommand
    {
        private GameModel model;

        public SuperStateCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Mario.changeToSuperState();
        }
    }
}