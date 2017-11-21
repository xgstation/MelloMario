namespace MelloMario.Controls.Commands
{
    internal class Left : BaseCommand<ICharacter>
    {
        public Left(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Left();
        }
    }
}
