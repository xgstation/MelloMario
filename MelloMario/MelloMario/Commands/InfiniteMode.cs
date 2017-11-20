namespace MelloMario.Factories
{
    internal class InfiniteMode : ICommand
    {
        private readonly IGameModel model;

        public InfiniteMode(IGameModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Infinite();
        }
    }
}
