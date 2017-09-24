using MelloMario.Commands;

namespace MelloMario
{
    internal class CrouchCommand : ICommand
    {
        private GameModel model;

        public CrouchCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Mario.down();
        }
    }
}