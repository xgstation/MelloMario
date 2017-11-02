namespace MelloMario.Commands
{
    class CrouchRelease : BaseCommand<ICharacter>
    {
        public CrouchRelease(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.CrouchRelease();
        }
    }
}
