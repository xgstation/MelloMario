namespace MelloMario.Commands
{
    class Right : BaseCommand<IGameControl>
    {
        public Right(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.Right();
        }
    }
}