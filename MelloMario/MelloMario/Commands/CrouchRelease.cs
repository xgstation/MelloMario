namespace MelloMario.Commands
{
    internal class CrouchRelease : BaseCommand<ICharacter>
    {
        public CrouchRelease(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.CrouchRelease();
        }
    }
}
