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
    class SuperCrouchingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;

        public SuperCrouchingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingLeft", setToStatic);

        }
        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireCrouchingLeft(mario);
        }

        public void changeToStandardState()
        {
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
            mario.State = new SuperIdleLeft(mario);
        }
        public void left()
        {
           //nothing here
        }

        public void right()
        {
            //right crouching
            mario.State = new SuperIdleRight(mario);
        }

        public void up()
        {
            mario.State = new SuperIdleLeft(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
