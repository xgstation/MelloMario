namespace MelloMario.Commands
{
    class RightReleaseCommand : BaseCommand<IGameCharacter>
    {
        public RightReleaseCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.RightRelease();
        }
    }
}