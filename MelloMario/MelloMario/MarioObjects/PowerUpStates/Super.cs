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
        Mario mario;

        public Super(Mario newMario)
        {
            mario = newMario;
        }
        public void UpgradeToFire()
        {
            this.mario.PowerUpState = new Fire(this.mario);
        }
        public void Downgrade()
        {
            this.mario.PowerUpState = new Standard(this.mario);
        }
        public void UpgradeToSuper()
        {

        }
        public override void Update(GameTime time)
        {

        }
    }
}
