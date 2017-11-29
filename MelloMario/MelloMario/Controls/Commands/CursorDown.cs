namespace MelloMario.Controls.Commands
{
    internal class CursorDown : BaseCommand<Game1>
    {
        public CursorDown(Game1 receiver) : base(receiver)
        {
        }

        public override void Execute()
        {
            Receiver.CursorDown();
        }
    }
}
