﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.States
{
    class SuperIdleRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        
        public SuperIdleRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperIdleRight",setToStatic);

        }
        public void down()
        {
            mario.State = new SuperCrouchingRight(mario);
        }
        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireIdleRight(mario); 
        }

        public void changeToStandardState()
        {
            mario.State = new StandardIdleRight(mario);
        }

        public void changeToSuperState()
        {
            //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }

        public void idle()
        {
            //nothing to do here
        }
        public void up()
        {
            mario.State = new SuperJumpingRight(mario);
        }

        public void right()
        {
            mario.State = new SuperWalkingRight(mario);
        }

        public void left()
        {
            //walk left
            mario.State = new SuperWalkingLeft(mario);
        }
    }
}