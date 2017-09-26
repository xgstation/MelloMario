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
    class SuperCrouchingRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;

        public SuperCrouchingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite =SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingRight", setToStatic);

        }
        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireCrouchingRight(mario);
        }

        public void changeToStandardState()
        {
            //nothing here
        }


        public void changeToSuperState()
        {
            //nothing to do here
        }

        public void down()
        {
            //nothing to do here
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
            //left crouching
            mario.State = new SuperCrouchingLeft(mario);
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.State = new SuperIdleRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
