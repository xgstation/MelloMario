namespace MelloMario.Commands
{
    class RightRelease : BaseCommand<ICharacter>
    {
        public RightRelease(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.RightRelease();
        }
    }
}
