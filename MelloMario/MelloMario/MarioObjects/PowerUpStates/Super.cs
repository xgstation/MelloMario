using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects.PowerUpStates
{
    internal class Super : BaseState<Mario>, IMarioPowerUpState
    {
        public Super(Mario owner) : base(owner)
        {
            SoundController.SizeUp.Play();
        }

        public void UpgradeToFire()
        {
            Owner.PowerUpState = new Fire(Owner);
            if (Owner.ProtectionState is Dead)
                Owner.ProtectionState = new Normal(Owner);
        }

        public void Downgrade()
        {
            Owner.PowerUpState = new Standard(Owner);
        }

        public void UpgradeToSuper() { }

        public override void Update(int time) { }
    }
}