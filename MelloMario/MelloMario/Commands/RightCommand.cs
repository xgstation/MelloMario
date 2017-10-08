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
            Receiver.userInX = Mario.H_MAX_ACCEL;
            Receiver.TurnRight();
        }
    }
}