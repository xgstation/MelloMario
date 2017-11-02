namespace MelloMario.Commands
{
    class Action : BaseCommand<IGameControl>
    {
        public Action(IGameControl control) : base(control)
        {
        }

        public override void Execute()
        {
            Receiver.Action();
        }
    }
}