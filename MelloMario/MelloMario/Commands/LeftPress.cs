namespace MelloMario.Commands
{
    class LeftPress : BaseCommand<IGameControl>
    {
        public LeftPress(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.LeftPress();
        }
    }
}
