namespace MelloMario.Commands
{
    class LeftPress : BaseCommand<ICharacter>
    {
        public LeftPress(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.LeftPress();
        }
    }
}
