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
    class FireJumpingLeft : IMarioState
    {
        Mario mario;
        bool setStatic;
        ISprite sprite;
        public FireJumpingLeft(Mario mario)
        {
            this.mario = mario;
            setStatic = true;
            sprite = SpriteFactory.Instance.CreateSprite("FireJumpingLeft", setStatic);


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
            mario.setMarioState(new StandardJumpingLeft(mario));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperJumpingLeft(mario));
        }

        public void down()
        {
            mario.setMarioState(new FireIdleLeft(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void idle()
        {
            mario.setMarioState(new FireIdleLeft(mario));
        }

        public void left()
        {
            //nothing to do here
        }

        public void right()
        {
            //right jump
            mario.setMarioState(new FireJumpingRight(mario));
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
