namespace MelloMario.Commands
{
    class RightPress : BaseCommand<IGameControl>
    {
        public RightPress(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.RightPress();
        }
    }
}
