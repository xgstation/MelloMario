namespace MelloMario.Commands
{
    class CrouchPress : BaseCommand<IGameCharacter>
    {
        public CrouchPress(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.CrouchPress();
        }
    }
}
