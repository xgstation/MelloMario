namespace MelloMario.Commands
{
    internal class UsedBlockCommand : ICommand
    {
        private GameModel model;

        public UsedBlockCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}