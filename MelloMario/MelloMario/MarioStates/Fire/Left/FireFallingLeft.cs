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
    class FireFallingLeft : IMarioState
    {
        Mario mario;
        bool setStatic;
        ISprite sprite;
        public FireFallingLeft(Mario mario)
        {
            this.mario = mario;
            setStatic = true;
            sprite = SpriteFactory.Instance.createSprite("FireFallingLeft",setStatic);
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario));   
        }

        public void changeToFireState()
        {
            //nothing here
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardFallingLeft(mario));
            
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperFallingLeft(mario));
        }

        public void down()
        {
            //nothing here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            //nothing here
        }

        public void idle()
        {
            //nothing here
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            mario.setMarioState(new FireFallingRight(mario));
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
