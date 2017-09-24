using MelloMario.Commands;

namespace MelloMario
{
    internal class FireStateCommand : ICommand
    {
        private GameModel model;

        public FireStateCommand(GameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            this.model.Mario.changeToFireState();
        }
    }
}