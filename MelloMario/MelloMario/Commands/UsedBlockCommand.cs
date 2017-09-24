using MelloMario.Commands;

namespace MelloMario
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