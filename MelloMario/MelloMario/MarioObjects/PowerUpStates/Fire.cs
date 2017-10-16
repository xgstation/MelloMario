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
        Mario mario;

        public Fire(Mario newMario)
        {
            mario = newMario;
        }
        public void UpgradeToFire()
        {

        }
        public void Downgrade()
        {
            this.mario.PowerUpState = new Standard(this.mario);

        }
        public void UpgradeToSuper()
        {
            this.mario.PowerUpState = new Super(this.mario);
        }
        public override void Update(GameTime time)
        {

        }
    }
}
