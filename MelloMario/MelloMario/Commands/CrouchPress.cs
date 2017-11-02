namespace MelloMario.Commands
{
    class CrouchPress : BaseCommand<IGameControl>
    {
        public CrouchPress(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.CrouchPress();
        }
    }
}
