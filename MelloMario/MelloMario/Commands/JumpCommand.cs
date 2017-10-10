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
            Receiver.userInY = -110;
            Receiver.Jump();
        }
    }
}