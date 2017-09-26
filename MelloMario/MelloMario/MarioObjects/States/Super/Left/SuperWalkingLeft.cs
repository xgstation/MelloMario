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
    class SuperWalkingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public SuperWalkingLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperWalkingLeft", setToStatic);
        }

        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireWalkingLeft(mario);
        }

        public void changeToStandardState()
        {
            mario.State = new StandardWalkingLeft(mario);
        }

        public void changeToSuperState()
        {
            //nothing here
        }

        public void down()
        {
            mario.State = new SuperCrouchingLeft(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void idle()
        {
            mario.State = new SuperIdleLeft(mario);
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //walk right
            mario.State = new SuperWalkingRight(mario);
        }

        public void up()
        {
            mario.State = new SuperJumpingLeft(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
