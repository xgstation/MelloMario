using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    class StandardJumpingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setStatic;
        ISprite sprite;
        public StandardJumpingLeft(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setStatic = true;
            sprite = SpriteFactory.Instance.createSprite("StandardJumpingLeft", setStatic, content);


        }

        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingLeft(mario,content));
        }

        public void changeToStandardState()
        {
            //nothing here
            
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperJumpingLeft(mario,content));
        }

        public void down()
        {
            mario.setMarioState(new StandardIdleLeft(mario,content));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void fall()
        {
            mario.setMarioState(new StandardFallingLeft(mario,content));
        }

        public void idle()
        {
            mario.setMarioState(new StandardIdleLeft(mario,content));   
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //jumping right
            mario.setMarioState(new StandardJumpingRight(mario,content));
        }

        public void up()
        {
            //nothing here
        }

        public void Update()
        {
            
        }
    }
}
