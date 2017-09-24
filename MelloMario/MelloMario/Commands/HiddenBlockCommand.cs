namespace MelloMario.Commands
{
    internal class HiddenBlockCommand : ICommand
    {
        private GameModel model;

        public HiddenBlockCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}