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
        ContentManager content;
        ISprite sprite;
        bool setToStatic;

        public StandardCrouchingRight(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("StandardCrouchingRight", setToStatic, this.content);
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireCrouchingRight(mario, content));
        }

        public void changeToStandardState()
        {
          //nothing to do here
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperCrouchingRight(mario, content));  
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
            mario.setMarioState(new StandardIdleRight(mario,content));
        }

        public void left()
        {
            mario.setMarioState(new StandardCrouchingLeft(mario,content));
        }

        public void right()
        {
            //nothing to do here
        }

        public void up()
        {
            mario.setMarioState(new StandardIdleRight(mario,content));
        }

        public void Update()
        {
         
        }
    }
}
