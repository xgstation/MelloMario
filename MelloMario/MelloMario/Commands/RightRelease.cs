namespace MelloMario.Commands
{
    class RightRelease : BaseCommand<IGameCharacter>
    {
        public RightRelease(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.RightRelease();
        }
    }
}