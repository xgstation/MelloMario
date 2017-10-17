namespace MelloMario.Commands
{
    class LeftReleaseCommand : BaseCommand<IGameCharacter>
    {
        public LeftReleaseCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.LeftRelease();
        }
    }
}