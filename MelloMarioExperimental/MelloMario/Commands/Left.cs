namespace MelloMario.Commands
{
    class Left : BaseCommand<ICharacter>
    {
        public Left(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Left();
        }
    }
}
