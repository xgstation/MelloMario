namespace MelloMario.Commands
{
    class LeftRelease : BaseCommand<ICharacter>
    {
        public LeftRelease(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.LeftRelease();
        }
    }
}