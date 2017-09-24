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
    class SuperCrouchingRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;

        public SuperCrouchingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite =SpriteFactory.Instance.createSprite("SuperCrouchingRight", setToStatic);

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
            mario.setMarioState(new StandardCrouchingRight(mario));
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
            mario.setMarioState(new SuperIdleRight(mario));
        }
        public void left()
        {
            //left crouching
            mario.setMarioState(new SuperCrouchingLeft(mario));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.setMarioState(new SuperIdleRight(mario));
        }

        public void Update(GameTime game)
        {
        }
    }
}
