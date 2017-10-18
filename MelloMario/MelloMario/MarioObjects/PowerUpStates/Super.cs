using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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
                Owner.ProtectionState = new ProtectionStates.Normal(Owner);
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
