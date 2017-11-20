namespace MelloMario.Controls.Commands
{
    internal class InfiniteMode : ICommand
    {
        private readonly IModel model;

        public InfiniteMode(IModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Infinite();
        }
    }
}
