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
    class StandardCrouchingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardCrouchingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateSprite("StandardCrouchingLeft", setToStatic);
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireCrouchingLeft(mario));
        }

        public void changeToStandardState()
        {
          //nothing to do here
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperCrouchingLeft(mario));  
        }

        public void down()
        {
           //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            //nothing to do here
        }

        public void idle()
        {
            mario.setMarioState(new StandardIdleLeft(mario));
        }

        public void left()
        {
            //left crouching
        }

        public void right()
        {
            //nothing to do here
        }

        public void up()
        {
            mario.setMarioState(new StandardIdleLeft(mario));
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
