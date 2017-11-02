namespace MelloMario.Commands
{
    class CrouchRelease : BaseCommand<IGameControl>
    {
        public CrouchRelease(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.CrouchRelease();
        }
    }
}
