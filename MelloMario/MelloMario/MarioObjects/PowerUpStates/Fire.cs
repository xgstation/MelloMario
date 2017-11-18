﻿using Microsoft.Xna.Framework.Audio;
using MelloMario.Sounds;

namespace MelloMario.MarioObjects.PowerUpStates
{
    class Fire : BaseState<Mario>, IMarioPowerUpState
    {
        public Fire(Mario owner) : base(owner)
        {
            SoundController.SizeUp.Play();
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
