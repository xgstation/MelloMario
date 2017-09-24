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
    class FireCrouchingRight : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setToStatic;
        ISprite sprite;
        public FireCrouchingRight(Mario mario,ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("FireCrouchingRight", setToStatic, content);
        }
        public void down()
        {
            //nothing to do here
        }
        //crouching
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            //nothing to do here
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardCrouchingRight(mario, content));
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperCrouchingRight(mario, content));           
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update()
        {
            
        }

        public void idle()
        {
            mario.setMarioState(new FireIdleRight(mario, content));
        }

        public void fall()
        {
            //nothing to do here           
        }

        public void up()
        {
            mario.setMarioState(new FireIdleRight(mario,content));
        }

        public void right()
        {
            //nothing to do here
        }

        public void left()
        {
            mario.setMarioState(new FireCrouchingLeft(mario,content));
        }
    }
}
