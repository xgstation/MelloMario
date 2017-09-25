using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.States
{
    class SuperIdleLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        
        public SuperIdleLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateSprite("SuperIdleLeft",setToStatic);

        }
        public void down()
        {
            mario.setMarioState(new SuperCrouchingLeft(mario));
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireIdleLeft(mario)); 
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardIdleLeft(mario));
        }

        public void changeToSuperState()
        {
            //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }

        public void idle()
        {
            //nothing to do here
        }

        public void up()
        {
            mario.setMarioState(new SuperJumpingLeft(mario));
        }

        public void right()
        {
            mario.setMarioState(new SuperWalkingRight(mario));
        }

        public void left()
        {
            mario.setMarioState(new SuperWalkingLeft(mario));
        }
    }
}
