namespace MelloMario.Commands
{
    class RightPress : BaseCommand<IGameCharacter>
    {
        public RightPress(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.RightPress();
        }
    }
}
