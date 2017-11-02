namespace MelloMario.Commands
{
    class LeftRelease : BaseCommand<IGameControl>
    {
        public LeftRelease(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.LeftRelease();
        }
    }
}