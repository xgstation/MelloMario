namespace MelloMario.Controls.Commands
{
    internal class FireCreate : BaseCommand<ICharacter>
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
