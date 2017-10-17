using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class StandardState : BaseCommand<Mario>
    {
        public StandardState(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            // TODO: this command should be replaced by MarioHurtCommand
            Receiver.Downgrade();
        }
    }
}