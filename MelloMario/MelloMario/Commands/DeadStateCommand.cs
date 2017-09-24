using MelloMario.Commands;

namespace MelloMario
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
            this.model.Mario.die();
        }
    }
}