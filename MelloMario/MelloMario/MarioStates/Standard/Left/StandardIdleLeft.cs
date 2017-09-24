﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    class StandardIdleLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardIdleLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("StandardIdleLeft",setToStatic);
        }
        public void down() {
            mario.setMarioState(new StandardCrouchingLeft(mario));
        }
        public void changeToFireState()
        {
            mario.setMarioState(new FireIdleLeft(mario));
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperIdleLeft(mario, content));   
        }
        public void changeToStandardState()
        {
            //nothing to do here
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }
        public void Update(GameTime game)
        {
            //Nothing to do here
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            
            sprite.Draw(spriteBatch,location);
            
        }

        public void idle()
        {
            //nothing to do here
        }

        public void fall()
        {
            //nothing to do here    
        }

        public void up()
        {
            mario.setMarioState(new StandardJumpingLeft(mario));
        }

        public void right()
        {
            mario.setMarioState(new StandardWalkingRight(mario));
        }

        public void left()
        {
            mario.setMarioState(new StandardWalkingLeft(mario));
        }
    }
}
