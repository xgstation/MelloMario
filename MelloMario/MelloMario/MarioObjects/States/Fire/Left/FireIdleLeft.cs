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
    class FireIdleLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
       
        public FireIdleLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", setToStatic);
        }
        public void down()
        {
            mario.State = new FireCrouchingLeft(mario);
        }
        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
        //nothing to do here
        }

        public void changeToStandardState()
        {
            mario.State = new StandardIdleLeft(mario);
        }

        public void changeToSuperState()
        {
            mario.State = new SuperIdleLeft(mario);
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
        public void up()
        {
            mario.State = new FireJumpingLeft(mario);
        }

        public void right()
        {
            mario.State = new FireWalkingRight(mario);
        }

        public void left()
        {
            mario.State = new FireWalkingLeft(mario);
        }
    }
}
