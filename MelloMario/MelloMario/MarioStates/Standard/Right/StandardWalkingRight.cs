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
    class StandardWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
        ContentManager content;

        public StandardWalkingRight(Mario newMario, ContentManager content)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.createSprite("StandardWalkingRight", setToStatic, content);
            this.content = content;
        }

        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireWalkingRight(mario,content));
        }

        public void changeToStandardState()
        {
            //nothing here
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperWalkingRight(mario,content));
        }

        public void down()
        {
            mario.setMarioState(new StandardCrouchingRight(mario,content));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            //nothing here
        }

        public void idle()
        {
            mario.setMarioState(new StandardIdleRight(mario,content));
        }

        public void jump()
        {
            mario.setMarioState(new StandardJumpingRight(mario,content));
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
            mario.setMarioState(new StandardJumpingRight(mario,content));
        }

        public void Update()
        {
            
        }
    }
}
