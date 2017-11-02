namespace MelloMario.Commands
{
    class Left : BaseCommand<IGameControl>
    {
        public Left(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.Left();
        }
    }
}