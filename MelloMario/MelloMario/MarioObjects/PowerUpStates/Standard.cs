using Microsoft.Xna.Framework;

namespace MelloMario.MarioObjects.PowerUpStates
{
    class Standard : BaseState<Mario>, IMarioPowerUpState
    {
        public Standard(Mario owner) : base(owner)
        {
        }

        public void UpgradeToFire()
        {
            Owner.PowerUpState = new Fire(Owner);
            if (Owner.ProtectionState is ProtectionStates.Dead)
            {
                Owner.ProtectionState = new ProtectionStates.Normal(Owner);
            }
        }

        public void Downgrade()
        {
            Owner.ProtectionState = new ProtectionStates.Dead(Owner);
        }

        public void UpgradeToSuper()
        {
            Owner.PowerUpState = new Super(Owner);
            if (Owner.ProtectionState is ProtectionStates.Dead)
            {
                Owner.ProtectionState = new ProtectionStates.Normal(Owner);
            }
        }

        public override void Update(int time)
        {
        }
    }
}
