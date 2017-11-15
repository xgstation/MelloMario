namespace MelloMario.Commands
{
    class FireCreate : BaseCommand<ICharacter>
    {
        public FireCreate(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.FireCreate();
        }
    }
}
