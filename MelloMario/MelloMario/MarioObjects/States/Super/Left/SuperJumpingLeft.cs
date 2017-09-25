using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.States
{
    class SuperJumpingLeft : IMarioState
    {
        Mario mario;
        bool setStatic;
        ISprite sprite;
        public SuperJumpingLeft(Mario mario)
        {
            this.mario = mario;
            setStatic = true;
            sprite = SpriteFactory.Instance.createSprite("SuperJumpingLeft", setStatic);


        }

        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingLeft(mario));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardJumpingLeft(mario));
        }


        public void changeToSuperState()
        {
         //nothing here  
        }

        public void down()
        {
            mario.setMarioState(new SuperIdleLeft(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            mario.setMarioState(new SuperFallingLeft(mario));
        }

        public void idle()
        {
            mario.setMarioState(new SuperIdleLeft(mario));   
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //jump right
            mario.setMarioState(new SuperJumpingRight(mario));
        }

        public void up()
        {
           //nothing here
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
