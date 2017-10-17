namespace MelloMario.Commands
{
    class Jump : BaseCommand<IGameCharacter>
    {
        public Jump(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Jump();
        }
    }
}