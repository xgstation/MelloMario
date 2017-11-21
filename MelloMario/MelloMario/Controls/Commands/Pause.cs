namespace MelloMario.Controls.Commands
{
    internal class Pause : BaseCommand<IModel>
    {
        public Pause(IModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.Pause();
        }
    }
}
