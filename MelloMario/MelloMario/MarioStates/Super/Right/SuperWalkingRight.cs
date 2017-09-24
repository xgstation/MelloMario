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
    class SuperWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
        ContentManager content;

        public SuperWalkingRight(Mario newMario, ContentManager content)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.createSprite("SuperWalkingRight", setToStatic, content);
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
            mario.setMarioState(new StandardWalkingRight(mario,content));
        }

        public void changeToSuperState()
        {
            //nothing here
        }

        public void down()
        {
            mario.setMarioState(new SuperCrouchingRight(mario,content));
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
            mario.setMarioState(new SuperIdleRight(mario,content));
        }

        public void left()
        {
            //walk left
            mario.setMarioState(new SuperWalkingLeft(mario,content));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.setMarioState(new SuperJumpingRight(mario,content));
        }

        public void Update()
        {
           
        }
    }
}
