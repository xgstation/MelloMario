namespace MelloMario.Commands
{
    class CrouchRelease : BaseCommand<IGameCharacter>
    {
        public CrouchRelease(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.CrouchRelease();
        }
    }
}
