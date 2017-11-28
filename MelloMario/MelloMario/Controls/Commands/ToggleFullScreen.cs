namespace MelloMario.Controls.Commands
{
    internal class ToggleFullScreen : BaseCommand<Game1>
    {
        public ToggleFullScreen(Game1 receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleFullScreen();
        }
    }
}
