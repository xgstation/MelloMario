using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    class FireIdleRight : IMarioState
    {
        Mario mario;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        ContentManager content;
        bool setToStatic;
       
        public FireIdleRight(Mario newMario, ContentManager newContent)
        {
            content = newContent;
            mario = newMario;
            setToStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("FireIdleRight",setToStatic,content);
        }
        public void down()
        {
            mario.setMarioState(new FireCrouchingRight(mario, content));
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
        //nothing to do here
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardIdleRight(mario,content));
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleIdleRight(mario, content));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperIdleRight(mario, content));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
         
            sprite.Draw(spriteBatch,location);
           
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
            mario.setMarioState(new FireJumpingRight(mario,content));
        }

        public void right()
        {
            mario.setMarioState(new FireWalkingRight(mario,content));
        }

        public void left()
        {
            mario.setMarioState(new FireWalkingLeft(mario,content));
        }
    }
}
