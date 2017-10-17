namespace MelloMario.Commands
{
    class LeftRelease : BaseCommand<IGameCharacter>
    {
        public LeftRelease(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.LeftRelease();
        }
    }
}