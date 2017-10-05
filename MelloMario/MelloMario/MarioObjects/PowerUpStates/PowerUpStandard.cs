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
    class PowerUpStandard : PowerUp
    {
        Mario mario;
        //ISprite sprite;
        //bool setToStatic;


        public PowerUpStandard(Mario newMario)
        {
            mario = newMario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", setToStatic);
        }
        public void UpgradeToPowerUpDead()
        {
            this.mario.State = new PowerUpDead(this.mario);
        }
        public void UpgradeToPowerUpFire()
        {
            this.mario.State = new PowerUpFire(this.mario);
        }
        public void UpgradeToPowerUpStandard()
        {

        }
        public void UpgradeToPowerUpSuper()
        {
            this.mario.State = new PowerUpSuper(this.mario);
        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
