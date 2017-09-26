using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class DeadStateCommand : BaseCommand<Mario>
    {
        public DeadStateCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.Die();
        }
    }
}