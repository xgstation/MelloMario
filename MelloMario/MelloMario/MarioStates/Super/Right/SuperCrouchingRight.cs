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
    class SuperCrouchingRight : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setToStatic;
        ISpriteFactory spriteCreation;
        ISprite sprite;

        public SuperCrouchingRight(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("SuperCrouchingRight", setToStatic, content);

        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireCrouchingRight(mario, content));
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleCrouchingRight(mario,content));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardCrouchingRight(mario,content));
        }


        public void changeToSuperState()
        {
            //nothing to do here
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
            mario.setMarioState(new SuperIdleRight(mario,content));
        }
        public void left()
        {
            //left crouching
            mario.setMarioState(new SuperCrouchingLeft(mario,content));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.setMarioState(new SuperIdleRight(mario,content));
        }

        public void Update()
        {
        }
    }
}
