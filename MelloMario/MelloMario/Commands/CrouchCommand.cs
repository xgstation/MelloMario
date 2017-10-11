namespace MelloMario.Commands
{
    class CrouchCommand : BaseCommand<IGameCharacter>
    {
        public CrouchCommand(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Crouch();
        }
    }
}