namespace MelloMario.Commands
{
    class RightRelease : BaseCommand<IGameControl>
    {
        public RightRelease(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.RightRelease();
        }
    }
}