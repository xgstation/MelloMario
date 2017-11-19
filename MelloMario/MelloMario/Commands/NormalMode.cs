namespace MelloMario.Factories
{
    internal class NormalMode : ICommand
    {
        private IGameModel model;

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