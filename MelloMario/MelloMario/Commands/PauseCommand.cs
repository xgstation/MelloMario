using MelloMario.Commands;

namespace MelloMario
{
    internal class PauseCommand : ICommand
    {

        private GameModel model;

        public PauseCommand(GameModel model)
        {
            this.model = model;
        }
        public void Execute()
        {
            //
        }
    }
}