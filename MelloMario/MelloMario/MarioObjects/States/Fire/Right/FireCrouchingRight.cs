﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.MarioObjects.States
{
    class FireCrouchingRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public FireCrouchingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireCrouchingRight", setToStatic);
        }
        public void down()
        {
            //nothing to do here
        }
        //crouching
        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            //nothing to do here
        }

        public void changeToStandardState()
        {
            //nothing here
        }
        public void changeToSuperState()
        {
            mario.State = new SuperCrouchingRight(mario);           
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
            mario.State = new FireIdleRight(mario);
        }

        public void up()
        {
            mario.State = new FireIdleRight(mario);
        }

        public void right()
        {
            //nothing to do here
        }

        public void left()
        {
            mario.State = new FireCrouchingLeft(mario);
        }
    }
}