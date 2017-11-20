namespace MelloMario.Controls.Commands
{
    internal class NormalMode : ICommand
    {
        private readonly IModel model;

        public NormalMode(IModel model)
        {
            this.model = model;
        }

        public void Execute()
        {
            model.Normal();
        }
    }
}
