using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class SuperStateCommand : BaseCommand<Mario>
    {
        public SuperStateCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.ChangeToSuperState();
        }
    }
}