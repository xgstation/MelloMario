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
            sprite = SpriteFactory.Instance.CreateMarioSprite("StandardJumpingRight", setStatic);

        }

        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireJumpingRight(mario);
        }

        public void changeToStandardState()
        {
            //nothing here
            
        }

        public void changeToSuperState()
        {
            mario.State = new SuperJumpingRight(mario);
        }

        public void down()
        {
            mario.State = new StandardIdleRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void idle()
        {
            mario.State = new StandardIdleRight(mario);   
        }

        public void left()
        {
            mario.State = new StandardJumpingLeft(mario);
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
