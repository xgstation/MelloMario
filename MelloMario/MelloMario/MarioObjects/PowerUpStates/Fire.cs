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
    class Fire : IMarioPowerUpState
    {
        Mario mario;
        //ISprite sprite;
        //bool setToStatic;


        public Fire(Mario newMario)
        {
            mario = newMario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", setToStatic);
        }
        public void Kill()
        {
            this.mario.PowerUpState = new Dead(this.mario);
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
        public void Update(GameTime time)
        {

        }
    }
}
