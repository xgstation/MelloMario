using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    class SuperIdleRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        ContentManager content;
        
        public SuperIdleRight(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            spriteCreation = new SpriteFactory();
            setToStatic = true;
            sprite = spriteCreation.createSprite("SuperIdleRight",setToStatic,this.content);

        }
        public void down()
        {
            mario.setMarioState(new SuperCrouchingRight(mario, content));
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireIdleRight(mario,content)); 
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardIdleRight(mario, content));
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleIdleRight(mario, content));
        }

        public void changeToSuperState()
        {
            //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update()
        {
        
        }

        public void idle()
        {
            //nothing to do here
        }

        public void fall()
        {
            //nothing to do here
        }

        public void up()
        {
            mario.setMarioState(new SuperJumpingRight(mario,content));
        }

        public void right()
        {
            mario.setMarioState(new SuperWalkingRight(mario,content));
        }

        public void left()
        {
            //walk left
            mario.setMarioState(new SuperWalkingLeft(mario,content));
        }
    }
}
