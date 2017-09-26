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
    class FireWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public FireWalkingRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireWalkingRight", setToStatic);
        }

        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            //nothing here
        }

        public void changeToStandardState()
        {
            mario.State = new StandardWalkingRight(mario);
        }

        public void changeToSuperState()
        {
            mario.State = new SuperWalkingRight(mario);
        }

        public void down()
        {
            mario.State = new FireCrouchingRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }
        public void idle()
        {
            mario.State = new FireIdleRight(mario);
        }

        public void left()
        {
            mario.State = new FireWalkingLeft(mario);
        }

        public void right()
        {
           //nothing here
        }

        public void up()
        {
            mario.State = new FireJumpingRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
