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
    class StandardCrouchingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardCrouchingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("StandardCrouchingRight", setToStatic);
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireCrouchingRight(mario));
        }

        public void changeToStandardState()
        {
          //nothing to do here
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperCrouchingRight(mario));  
        }

        public void down()
        {
           //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            //nothing to do here
        }

        public void idle()
        {
            mario.setMarioState(new StandardIdleRight(mario));
        }

        public void left()
        {
            mario.setMarioState(new StandardCrouchingLeft(mario));
        }

        public void right()
        {
            //nothing to do here
        }

        public void up()
        {
            mario.setMarioState(new StandardIdleRight(mario));
        }

        public void Update(GameTime game)
        {
         
        }
    }
}
