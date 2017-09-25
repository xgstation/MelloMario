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
    class StandardJumpingRight : IMarioState
    {
        Mario mario;
        bool setStatic;
        ISprite sprite;
        public StandardJumpingRight(Mario mario)
        {
            this.mario = mario;
            setStatic = true;
            sprite = SpriteFactory.Instance.CreateSprite("StandardJumpingRight", setStatic);

        }

        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingRight(mario));
        }

        public void changeToStandardState()
        {
            //nothing here
            
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperJumpingRight(mario));
        }

        public void down()
        {
            mario.setMarioState(new StandardIdleRight(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void idle()
        {
            mario.setMarioState(new StandardIdleRight(mario));   
        }

        public void left()
        {
            mario.setMarioState(new StandardJumpingLeft(mario));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            //nothing here
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
