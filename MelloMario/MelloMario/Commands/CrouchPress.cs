namespace MelloMario.Commands
{
    internal class CrouchPress : BaseCommand<ICharacter>
    {
        public CrouchPress(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.CrouchPress();
        }
    }
}
