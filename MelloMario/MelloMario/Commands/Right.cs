namespace MelloMario.Commands
{
    class Right : BaseCommand<IGameCharacter>
    {
        public Right(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Right();
        }
    }
}