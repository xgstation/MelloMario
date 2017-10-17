namespace MelloMario.Commands
{
    class Left : BaseCommand<IGameCharacter>
    {
        public Left(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Left();
        }
    }
}