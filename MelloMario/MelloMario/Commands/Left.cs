namespace MelloMario.Commands
{
    internal class Left : BaseCommand<ICharacter>
    {
        public Left(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.Left();
        }
    }
}
