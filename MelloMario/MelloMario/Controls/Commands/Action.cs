namespace MelloMario.Controls.Commands
{
    internal class Action : BaseCommand<ICharacter>
    {
        public Action(ICharacter character) : base(character) { }

        public override void Execute()
        {
            Receiver.Action();
        }
    }
}
