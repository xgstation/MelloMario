namespace MelloMario.Commands
{
    class JumpRelease : BaseCommand<IGameCharacter>
    {
        public JumpRelease(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.JumpRelease();
        }
    }
}
