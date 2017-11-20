namespace MelloMario.Controls.Commands
{
    internal class Right : BaseCommand<ICharacter>
    {
        public Right(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.Right();
        }
    }
}
