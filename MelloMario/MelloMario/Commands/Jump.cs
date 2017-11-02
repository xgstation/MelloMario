namespace MelloMario.Commands
{
    class Jump : BaseCommand<IGameControl>
    {
        public Jump(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.Jump();
        }
    }
}