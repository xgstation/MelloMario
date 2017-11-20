namespace MelloMario.Controls.Commands
{
    internal class ToggleFullScreen : BaseCommand<IModel>
    {
        public ToggleFullScreen(IModel receiver) : base(receiver) { }

        public override void Execute()
        {
            Receiver.ToggleFullScreen();
        }
    }
}
