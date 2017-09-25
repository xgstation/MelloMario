namespace MelloMario.Commands
{
    internal class BrickBlockCommand : ICommand
    {
        private GameModel model;

        public BrickBlockCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}