using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class DeadState : BaseCommand<Mario>
    {
        public DeadState(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.Kill();
        }
    }
}