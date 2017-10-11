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
    class Standard : IMarioPowerUpState
    {
        Mario mario;

        public Standard(Mario newMario)
        {
            mario = newMario;
        }
        public void UpgradeToFire()
        {
            this.mario.PowerUpState = new Fire(this.mario);
        }
        public void Downgrade()
        {

        }
        public void UpgradeToSuper()
        {
            this.mario.PowerUpState = new Super(this.mario);
        }
        public void Update(GameTime time)
        {

        }
    }
}
