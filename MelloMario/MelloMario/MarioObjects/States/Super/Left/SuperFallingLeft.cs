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
    class SuperFallingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;

        public SuperFallingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("SuperFallingLeft",setToStatic);
        }
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
            mario.setMarioState(new StandardFallingLeft(mario));
        }

        public void changeToSuperState()
        {

        }

        public void down()
        {
            //nothin here
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
            //right falling
            mario.setMarioState(new SuperFallingRight(mario));
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
