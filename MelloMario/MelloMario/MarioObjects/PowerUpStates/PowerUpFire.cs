﻿using System;
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
    class PowerUpFire : IMarioPowerUpState
    {
        Mario mario;
        //ISprite sprite;
        //bool setToStatic;


        public PowerUpFire (Mario newMario)
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

        }
       public void UpgradeToPowerUpStandard()
        {
            this.mario.State = new PowerUpStandard(this.mario);

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
