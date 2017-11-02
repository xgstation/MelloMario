namespace MelloMario.Commands
{
    class JumpRelease : BaseCommand<IGameControl>
    {
        public JumpRelease(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.JumpRelease();
        }
    }
}
