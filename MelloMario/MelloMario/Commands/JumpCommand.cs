namespace MelloMario.Commands
{
    class JumpCommand : BaseCommand<IGameCharacter>
    {
        public JumpCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Jump();
        }
    }
}