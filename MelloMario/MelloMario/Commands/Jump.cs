namespace MelloMario.Commands
{
    class Jump : BaseCommand<ICharacter>
    {
        public Jump(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Jump();
        }
    }
}
