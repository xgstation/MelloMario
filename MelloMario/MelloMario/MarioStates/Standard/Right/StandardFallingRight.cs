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
    class StandardFallingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
        public StandardFallingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("StandardFallingRight",setToStatic);
        }
        //falling
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireFallingRight(mario));
        }

        public void changeToStandardState()
        {
         //nothing to do here
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperFallingRight(mario));
        }

        public void down()
        {
         //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void fall()
        {
            //nothing to do here
        }

        public void idle()
        {
            //nothing to do here
        }

        public void left()
        {
            mario.setMarioState(new StandardFallingLeft(mario));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            //nothing here
        }

        public void Update(GameTime game)
        {
            
        }
    }
}
