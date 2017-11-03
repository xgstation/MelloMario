using Microsoft.Xna.Framework;

namespace MelloMario.MarioObjects.PowerUpStates
{
    class Super : BaseState<Mario>, IMarioPowerUpState
    {
        public Super(Mario owner) : base(owner)
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
            Owner.PowerUpState = new Standard(Owner);
        }

        public void UpgradeToSuper()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}
