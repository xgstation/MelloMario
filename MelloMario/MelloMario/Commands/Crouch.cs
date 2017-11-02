namespace MelloMario.Commands
{
    class Crouch : BaseCommand<IGameControl>
    {
        public Crouch(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.Crouch();
        }
    }
}