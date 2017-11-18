namespace MelloMario.Commands
{
    internal class LeftRelease : BaseCommand<ICharacter>
    {
        public LeftRelease(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.LeftRelease();
        }
    }
}