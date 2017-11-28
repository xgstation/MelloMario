namespace MelloMario.Controls.Commands
{
    internal class KillMario : BaseCommand<ICharacter>
    {
        public KillMario(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.KillMario();
        }
    }
}
