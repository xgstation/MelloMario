namespace MelloMario.Commands
{
    class Crouch : BaseCommand<ICharacter>
    {
        public Crouch(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Crouch();
        }
    }
}
