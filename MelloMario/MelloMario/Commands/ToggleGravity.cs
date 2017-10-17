using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class ToggleGravity : BaseCommand<Mario>
    {
        public ToggleGravity(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.ToggleGravity();
        }
    }
}