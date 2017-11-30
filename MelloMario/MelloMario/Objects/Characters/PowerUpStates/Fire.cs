namespace MelloMario.Objects.Characters.PowerUpStates
{
    #region

    using System;
    using MelloMario.Objects.Characters.ProtectionStates;

    #endregion

    internal class Fire : BaseState<Mario>, IMarioPowerUpState
    {
        public Fire(Mario owner) : base(owner)
        {
            //TODO:Move this into soundcontroller
            //SoundManager.SizeUp.Play();
        }

        public void UpgradeToFire()
        {
        }

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

        public override void Update(int time)
        {
        }
    }
}
