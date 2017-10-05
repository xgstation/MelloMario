using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class CrouchCommand : BaseCommand<Mario>
    {
        public CrouchCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.Crouch();
        }
    }
}