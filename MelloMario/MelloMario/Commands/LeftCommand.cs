using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class LeftCommand : BaseCommand<Mario>
    {
        public LeftCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.userInX = -1*Mario.H_MAX_ACCEL;
        }
    }
}