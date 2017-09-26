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
    class FireJumpingRight : IMarioState
    {
        Mario mario;
        bool setStatic;
        ISprite sprite;
        public FireJumpingRight(Mario mario)
        {
            this.mario = mario;
            setStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireJumpingRight", setStatic);
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
            mario.setMarioState(new StandardJumpingRight(mario));
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperJumpingRight(mario));
        }

        public void down()
        {
            mario.setMarioState(new FireIdleRight(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void idle()
        {
            mario.setMarioState(new FireIdleRight(mario));
        }

        public void left()
        {
            mario.setMarioState(new FireIdleLeft(mario));
        }

        public void right()
        {
            //nothing to do here
        }

        public void up()
        {
            //nothing to do here
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
