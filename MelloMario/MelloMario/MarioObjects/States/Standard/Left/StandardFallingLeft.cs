using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.MarioObjects.States
{
    class StandardFallingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
        public StandardFallingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateSprite("StandardFallingLeft",setToStatic);
        }
        //falling
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireFallingLeft(mario));
        }

        public void changeToStandardState()
        {
         //nothing to do here
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperFallingLeft(mario));
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
            //nothing to do here
        }

        public void right()
        {
            //right falling
            mario.setMarioState(new StandardFallingRight(mario));
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
