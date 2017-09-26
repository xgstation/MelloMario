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
    class FireWalkingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public FireWalkingLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireWalkingLeft", setToStatic);
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
            mario.State = new StandardWalkingLeft(mario);
        }

        public void changeToSuperState()
        {
            mario.State = new SuperWalkingLeft(mario);
        }

        public void down()
        {
            mario.State = new FireCrouchingLeft(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }


        public void idle()
        {
            mario.State = new FireIdleLeft(mario);
        }

        public void left()
        {
           //nothing here
        }

        public void right()
        {
            //walk right
            mario.State = new FireWalkingRight(mario);
        }

        public void up()
        {
            mario.State = new FireJumpingLeft(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
