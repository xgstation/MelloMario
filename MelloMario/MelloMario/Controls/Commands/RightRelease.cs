namespace MelloMario.Controls.Commands
{
    internal class RightRelease : BaseCommand<ICharacter>
    {
        public RightRelease(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.RightRelease();
        }
    }
}
