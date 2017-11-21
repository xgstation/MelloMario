namespace MelloMario.Controls.Commands
{
    internal class Reset : BaseCommand<IModel>
    {
        public Reset(IModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.Reset();
        }
    }
}
