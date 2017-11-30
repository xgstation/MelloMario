namespace MelloMario.Objects.Characters.PowerUpStates
{
    #region

    using System;
    using MelloMario.Objects.Characters.ProtectionStates;

    #endregion

    internal class Super : BaseState<Mario>, IMarioPowerUpState
    {
        public Super(Mario owner) : base(owner)
        {
            //TODO:Move this into soundcontroller
            //SoundManager.SizeUp.Play();
        }

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
            Owner.PowerUpState = new Standard(Owner);
        }

        public void UpgradeToSuper()
        {
        }

        public override void Update(int time)
        {
        }
    }
}
