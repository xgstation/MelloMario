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
    class SuperCrouchingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setToStatic;
        ISprite sprite;

        public SuperCrouchingLeft(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("SuperCrouchingLeft", setToStatic, content);

        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireCrouchingLeft(mario, content));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardCrouchingLeft(mario,content));
        }

        public void changeToSuperState()
        {
            //nothing to do here
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
            mario.setMarioState(new SuperIdleLeft(mario,content));
        }
        public void left()
        {
           //nothing here
        }

        public void right()
        {
            //right crouching
            mario.setMarioState(new SuperIdleRight(mario,content));
        }

        public void up()
        {
            mario.setMarioState(new SuperIdleLeft(mario,content));
        }

        public void Update()
        {
        }
    }
}
