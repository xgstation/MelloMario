namespace MelloMario.Commands
{
    class NormalCreate : BaseCommand<ICharacter>
    {
        public NormalCreate(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.NormalCreate();
        }
    }
}
