namespace MelloMario.Commands
{
    class UsedBlockCommand : BaseCommand<IGameObject>
    {
        public UsedBlockCommand(IGameObject obj): base(obj)
        {
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}