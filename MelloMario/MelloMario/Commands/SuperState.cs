using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class SuperState : BaseCommand<Mario>
    {
        public SuperState(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.UpgradeToSuper();
        }
    }
}