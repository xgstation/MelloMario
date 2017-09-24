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
    class StandardFallingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
        ContentManager content;
        public StandardFallingLeft(Mario mario,ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("StandardFallingLeft",setToStatic,content);
        }
        //falling
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireFallingLeft(mario,content));
        }

        public void changeToStandardState()
        {
         //nothing to do here
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperFallingLeft(mario, content));
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
            mario.setMarioState(new StandardFallingRight(mario,content));
        }

        public void up()
        {
            //nothing here
        }

        public void Update()
        {
            
        }
    }
}
