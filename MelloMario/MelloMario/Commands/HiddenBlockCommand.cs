namespace MelloMario.Commands
{
    class HiddenBlockCommand : BaseCommand<IGameObject>
    {
        public HiddenBlockCommand(IGameObject obj) : base(obj)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}