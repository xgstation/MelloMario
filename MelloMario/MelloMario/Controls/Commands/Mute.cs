namespace MelloMario.Controls.Commands
{
    internal class Mute : BaseCommand<IModel>
    {
        public Mute(IModel model) : base(model)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleMute();
        }
    }
}
