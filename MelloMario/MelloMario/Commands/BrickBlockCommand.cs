namespace MelloMario.Commands
{
    internal class BrickBlockCommand : BaseCommand<IGameObject>
    {
        public BrickBlockCommand(IGameObject obj) : base(obj)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}