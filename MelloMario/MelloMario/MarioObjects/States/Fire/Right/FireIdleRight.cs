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
    class FireIdleRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
       
        public FireIdleRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("FireIdleRight",setToStatic);
        }
        public void down()
        {
            mario.setMarioState(new FireCrouchingRight(mario));
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
        //nothing to do here
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardIdleRight(mario));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperIdleRight(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
         
            sprite.Draw(spriteBatch,location);
           
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }

        public void idle()
        {
            //nothing to do here
        }

        public void fall()
        {
            //nothing to do here
        }

        public void up()
        {
            mario.setMarioState(new FireJumpingRight(mario));
        }

        public void right()
        {
            mario.setMarioState(new FireWalkingRight(mario));
        }

        public void left()
        {
            mario.setMarioState(new FireWalkingLeft(mario));
        }
    }
}
