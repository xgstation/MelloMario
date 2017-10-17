namespace MelloMario.Commands
{
    class JumpPress : BaseCommand<IGameCharacter>
    {
        public JumpPress(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.JumpPress();
        }
    }
}
