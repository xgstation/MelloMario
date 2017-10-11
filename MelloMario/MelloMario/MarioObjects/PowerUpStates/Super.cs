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
    class Super : IMarioPowerUpState
    {
        Mario mario;
        //ISprite sprite;
        //bool setToStatic;


        public Super(Mario newMario)
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
            this.mario.PowerUpState = new Fire(this.mario);
        }
        public void Downgrade()
        {
            this.mario.PowerUpState = new Standard(this.mario);
        }
        public void UpgradeToSuper()
        {

        }
        public void Update(GameTime time)
        {

        }
    }
}
