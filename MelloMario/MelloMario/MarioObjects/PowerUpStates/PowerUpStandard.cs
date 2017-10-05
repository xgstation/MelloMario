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
    class PowerUpStandard : IMarioPowerUpState
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
        public void Kill()
        {
            this.mario.State = new PowerUpDead(this.mario);
        }
        public void UpgradeToFire()
        {
            this.mario.State = new PowerUpFire(this.mario);
        }
        public void Downgrade()
        {

        }
        public void UpgradeToSuper()
        {
            this.mario.State = new PowerUpSuper(this.mario);
        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
