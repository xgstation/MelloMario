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
    class Dead : IMarioPowerUpState
    {
        Mario mario;
        //ISprite sprite;
        //bool setToStatic;


        public Dead(Mario newMario)
        {
            mario = newMario;
            //setToStatic = true;
            //sprite = SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", setToStatic);
        }
        public void Kill()
        {

        }
        public void UpgradeToFire()
        {

        }
        public void Downgrade()
        {

        }
        public void UpgradeToSuper()
        {

        }
        public void Update(GameTime time)
        {

        }
    }
}

