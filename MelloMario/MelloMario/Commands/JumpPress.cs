namespace MelloMario.Commands
{
    class JumpPress : BaseCommand<IGameControl>
    {
        public JumpPress(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.JumpPress();
        }
    }
}
