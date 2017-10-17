using MelloMario.MarioObjects;

namespace MelloMario.Commands
{
    class FireState : BaseCommand<Mario>
    {
        public FireState(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            Receiver.UpgradeToFire();
        }
    }
}