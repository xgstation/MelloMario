namespace MelloMario.Controls.Commands
{
    internal class CursorUp : BaseCommand<Game1>
    {
        public CursorUp(Game1 receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.CursorUp();
        }
    }
}
