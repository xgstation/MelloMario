namespace MelloMario.Commands
{
    class JumpPress : BaseCommand<ICharacter>
    {
        public JumpPress(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.JumpPress();
        }
    }
}
