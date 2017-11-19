using MelloMario.MarioObjects.ProtectionStates;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects.PowerUpStates
{
    internal class Fire : BaseState<Mario>, IMarioPowerUpState
    {
        public Fire(Mario owner) : base(owner)
        {
            SoundController.SizeUp.Play();
        }

        public void UpgradeToFire() { }

        public void Downgrade()
        {
            UpgradeToSuper();
        }

        public void UpgradeToSuper()
        {
            Owner.PowerUpState = new Super(Owner);
            if (Owner.ProtectionState is Dead)
            {
                Owner.ProtectionState = new Normal(Owner);
            }
        }

        public override void Update(int time) { }
    }
}