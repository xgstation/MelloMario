using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.MarioStates
{
    class FireWalkingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public FireWalkingLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.createSprite("FireWalkingLeft", setToStatic);
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
            mario.setMarioState(new StandardWalkingLeft(mario));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperWalkingLeft(mario));
        }

        public void down()
        {
            mario.setMarioState(new FireCrouchingLeft(mario));
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
            mario.setMarioState(new FireIdleLeft(mario));
        }

        public void left()
        {
           //nothing here
        }

        public void right()
        {
            //walk right
            mario.setMarioState(new FireWalkingRight(mario));
        }

        public void up()
        {
            mario.setMarioState(new FireJumpingLeft(mario));
        }

        public void Update(GameTime game)
        {
            
        }
    }
}
