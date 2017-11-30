namespace MelloMario.Controls.Commands
{
    internal class ExitModel : BaseCommand<IModel>
    {
        public ExitModel(IModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.Exit();
        }
    }
}
