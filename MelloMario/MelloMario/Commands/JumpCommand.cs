using MelloMario.Commands;

namespace MelloMario
{
    internal class JumpCommand : ICommand
    {
        private GameModel model;

        public JumpCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Mario.up();
        }
    }
}