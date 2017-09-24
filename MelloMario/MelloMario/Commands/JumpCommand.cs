using MelloMario.Commands;
using System.Diagnostics;

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
            this.model.Mario.up();
        }
    }
}