using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class RightCommand : BaseCommand<Mario>
    {
        public RightCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.right();
        }
    }
}