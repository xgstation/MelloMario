namespace MelloMario.Commands
{
    class ActionCommand : BaseCommand<IGameCharacter>
    {
        public ActionCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Action();
        }
    }
}