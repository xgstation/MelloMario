using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class ActionCommand : BaseCommand<Mario>
    {
        public ActionCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            // Receiver.fire();
        }
    }
}