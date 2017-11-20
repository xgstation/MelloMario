using MelloMario.Objects.Characters.ProtectionStates;

namespace MelloMario.Objects.Characters.PowerUpStates
{
    internal class Standard : BaseState<Mario>, IMarioPowerUpState
    {
        public Standard(Mario owner) : base(owner) { }

        public void UpgradeToFire()
        {
            Owner.PowerUpState = new Fire(Owner);
            if (Owner.ProtectionState is Dead)
            {
                Owner.ProtectionState = new Normal(Owner);
            }
        }

        public void Downgrade()
        {
            Owner.ProtectionState = new Dead(Owner);
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
