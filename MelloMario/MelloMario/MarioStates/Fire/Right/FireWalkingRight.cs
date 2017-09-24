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
    class FireWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public FireWalkingRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.createSprite("FireWalkingRight", setToStatic);
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
            mario.setMarioState(new StandardWalkingRight(mario));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperWalkingRight(mario));
        }

        public void down()
        {
            mario.setMarioState(new FireCrouchingRight(mario));
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
            mario.setMarioState(new FireIdleRight(mario));
        }

        public void left()
        {
            mario.setMarioState(new FireWalkingLeft(mario));
        }

        public void right()
        {
           //nothing here
        }

        public void up()
        {
            mario.setMarioState(new FireJumpingRight(mario));
        }

        public void Update(GameTime game)
        {
            
        }
    }
}
