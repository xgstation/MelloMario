using MelloMario.Commands;

namespace MelloMario
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