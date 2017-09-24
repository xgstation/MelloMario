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
    class StandardJumpingRight : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setStatic;
        ISprite sprite;
        public StandardJumpingRight(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setStatic = true;
            sprite = SpriteFactory.Instance.createSprite("StandardJumpingRight", setStatic, content);

        }

        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingRight(mario,content));
        }

        public void changeToStandardState()
        {
            //nothing here
            
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperJumpingRight(mario,content));
        }

        public void down()
        {
            mario.setMarioState(new StandardIdleRight(mario,content));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void fall()
        {
            mario.setMarioState(new StandardFallingRight(mario,content));
        }

        public void idle()
        {
            mario.setMarioState(new StandardIdleRight(mario,content));   
        }

        public void left()
        {
            mario.setMarioState(new StandardJumpingLeft(mario,content));
        }

        public void right()
        {
            //nothing here
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
