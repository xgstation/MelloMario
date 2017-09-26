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
    class SuperWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public SuperWalkingRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperWalkingRight", setToStatic);
        }

        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireWalkingRight(mario);
        }

        public void changeToStandardState()
        {
            mario.State = new StandardWalkingRight(mario);
        }

        public void changeToSuperState()
        {
            //nothing here
        }

        public void down()
        {
            mario.State = new SuperCrouchingRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }
        public void idle()
        {
            mario.State = new SuperIdleRight(mario);
        }

        public void left()
        {
            //walk left
            mario.State = new SuperWalkingLeft(mario);
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.State = new SuperJumpingRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
