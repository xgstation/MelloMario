namespace MelloMario.Commands
{
    class RightPress : BaseCommand<ICharacter>
    {
        public RightPress(ICharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.RightPress();
        }
    }
}
