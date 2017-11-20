namespace MelloMario.Commands
{
    internal class NormalCreate : BaseCommand<ICharacter>
    {
        public NormalCreate(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.NormalCreate();
        }
    }
}
