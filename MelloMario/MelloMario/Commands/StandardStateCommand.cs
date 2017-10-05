using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class StandardStateCommand : BaseCommand<Mario>
    {
        public StandardStateCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            // TODO: this command should be replaced by MarioHurtCommand
            Receiver.Downgrade();
        }
    }
}