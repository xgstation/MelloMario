using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class JumpCommand : BaseCommand<Mario>
    {
        public JumpCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.userInY =  Mario.J_MAX_ACCEL;
            Receiver.Jump();
        }
    }
}