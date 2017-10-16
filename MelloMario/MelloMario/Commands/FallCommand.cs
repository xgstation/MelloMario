using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class FallCommand : BaseCommand<Mario>
    {
        public FallCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.Fall();
        }
    }
}