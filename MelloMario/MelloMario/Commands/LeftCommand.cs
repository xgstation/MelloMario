namespace MelloMario.Commands
{
    class LeftCommand : BaseCommand<IGameCharacter>
    {
        public LeftCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Left();
        }
    }
}