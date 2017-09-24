using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario 
{
    class FireCrouchingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public FireCrouchingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("FireCrouchingLeft", setToStatic);
        }
        public void down()
        {
            //nothing to do here
        }
        //crouching
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            //nothing to do here
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardCrouchingLeft(mario));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperCrouchingLeft(mario));           
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime game)
        {
            
        }

        public void idle()
        {
            mario.setMarioState(new FireIdleLeft(mario));
        }

        public void fall()
        {
            //nothing to do here           
        }

        public void up()
        {
            mario.setMarioState(new FireIdleLeft(mario));
        }

        public void right()
        {
            mario.setMarioState(new FireCrouchingRight(mario));
        }

        public void left()
        {
            //left crouch
        }
    }
}
