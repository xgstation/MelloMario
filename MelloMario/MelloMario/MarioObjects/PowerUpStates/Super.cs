using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects.PowerUpStates
{
    class Super : BaseState<Mario>, IMarioPowerUpState
    {
        private SoundEffectInstance powerupSound;

        public Super(Mario owner) : base(owner)
        {
            powerupSound = SoundController.sizeUp.CreateInstance();
            powerupSound.Play();
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

        public override void Update(int time)
        {
        }
    }
}
