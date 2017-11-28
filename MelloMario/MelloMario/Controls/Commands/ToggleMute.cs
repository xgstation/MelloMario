namespace MelloMario.Controls.Commands
{
    internal class ToggleMute : BaseCommand<Game1>
    {
        public ToggleMute(Game1 game) : base(game)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleMute();
        }
    }
}
