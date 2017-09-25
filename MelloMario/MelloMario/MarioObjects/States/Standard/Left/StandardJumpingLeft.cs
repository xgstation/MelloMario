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
    class StandardJumpingLeft : IMarioState
    {
        Mario mario;
        bool setStatic;
        ISprite sprite;
        public StandardJumpingLeft(Mario mario)
        {
            this.mario = mario;
            setStatic = true;
            sprite = SpriteFactory.Instance.CreateSprite("StandardJumpingLeft", setStatic);


        }

        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingLeft(mario));
        }

        public void changeToStandardState()
        {
            //nothing here
            
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperJumpingLeft(mario));
        }

        public void down()
        {
            mario.setMarioState(new StandardIdleLeft(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void idle()
        {
            mario.setMarioState(new StandardIdleLeft(mario));   
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //jumping right
            mario.setMarioState(new StandardJumpingRight(mario));
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
