namespace MelloMario.Controls.Commands
{
    internal class JumpRelease : BaseCommand<ICharacter>
    {
        public JumpRelease(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.JumpRelease();
        }
    }
}
