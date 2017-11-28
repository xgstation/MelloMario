namespace MelloMario.Controls.Commands
{
    internal class Exit : BaseCommand<Game1>
    {
        public Exit(Game1 game) : base(game)
        {
        }

        public override void Execute()
        {
            Receiver.Exit();
        }
    }
}
