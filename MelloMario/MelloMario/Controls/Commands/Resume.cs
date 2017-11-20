namespace MelloMario.Controls.Commands
{
    internal class Resume : BaseCommand<IModel>
    {
        public Resume(IModel model) : base(model) { }

        public override void Execute()
        {
            Receiver.Resume();
        }
    }
}
