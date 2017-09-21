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
    class StandardCrouchingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        bool setToStatic;

        public StandardCrouchingLeft(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("StandardCrouchingLeft", setToStatic, this.content);
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireCrouchingLeft(mario, content));
        }

        public void changeToStandardState()
        {
          //nothing to do here
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleCrouchingLeft(mario, content));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperCrouchingLeft(mario, content));  
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
            mario.setMarioState(new StandardIdleLeft(mario,content));
        }

        public void left()
        {
            //left crouching
        }

        public void right()
        {
            //nothing to do here
        }

        public void up()
        {
            mario.setMarioState(new StandardIdleLeft(mario,content));
        }

        public void Update()
        {
         
        }
    }
}
