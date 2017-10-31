using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.PowerUpStates
{
    class Fire : BaseState<Mario>, IMarioPowerUpState
    {
        public Fire(Mario owner) : base(owner)
        {
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
                Owner.ProtectionState = new ProtectionStates.Normal(Owner);
        }

        public override void Update(GameTime time)
        {

        }
    }
}
