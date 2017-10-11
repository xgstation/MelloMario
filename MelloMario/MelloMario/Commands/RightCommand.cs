namespace MelloMario.Commands
{
    class RightCommand : BaseCommand<IGameCharacter>
    {
        public RightCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Right();
        }
    }
}