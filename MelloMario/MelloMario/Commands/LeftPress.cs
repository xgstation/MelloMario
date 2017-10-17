namespace MelloMario.Commands
{
    class LeftPress : BaseCommand<IGameCharacter>
    {
        public LeftPress(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.LeftPress();
        }
    }
}
