namespace MelloMario.Commands
{
    class Crouch : BaseCommand<IGameCharacter>
    {
        public Crouch(IGameCharacter character) : base(character)
        {
        }

        public override void Execute()
        {
            Receiver.Crouch();
        }
    }
}