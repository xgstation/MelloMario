using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class ToggleGravityCommand : BaseCommand<Mario>
    {
        public ToggleGravityCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleGravity();
        }
    }
}