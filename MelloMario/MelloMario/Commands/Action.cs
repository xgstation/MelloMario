namespace MelloMario.Commands
{
    class Action : BaseCommand<IGameCharacter>
    {
        public Action(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Action();
        }
    }
}