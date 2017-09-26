using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class FireStateCommand : BaseCommand<Mario>
    {
        public FireStateCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.ChangeToFireState();
        }
    }
}