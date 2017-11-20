namespace MelloMario.Controls.Commands
{
    internal class NormalMode : ICommand
    {
        private readonly IGameModel model;

        public NormalMode(IGameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Normal();
        }
    }
}
