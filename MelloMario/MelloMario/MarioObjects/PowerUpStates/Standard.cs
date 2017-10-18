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
    class Standard : BaseState<Mario>, IMarioPowerUpState
    {
        public Standard(Mario owner) : base(owner)
        {
        }

        public void UpgradeToFire()
        {
            Owner.PowerUpState = new Fire(Owner);
        }
        public void Downgrade()
        {
            Owner.ProtectionState = new ProtectionStates.Dead(Owner);
        }
        public void UpgradeToSuper()
        {
            Owner.PowerUpState = new Super(Owner);
        }
        public override void Update(GameTime time)
        {

        }
    }
}
