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
    class InvincibleIdleLeft : IMarioState
    {
        Mario mario;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        ContentManager content;
        bool setToStatic;
       
        public InvincibleIdleLeft(Mario newMario, ContentManager newContent)
        {
            content = newContent;
            mario = newMario;
            setToStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("InvincibleIdleLeft",setToStatic,content);
        }
        public void down()
        {
            mario.setMarioState(new InvincibleCrouchingLeft(mario, content));
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
          
            mario.setMarioState(new FireIdleLeft(mario, content));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardIdleLeft(mario,content));
        }

        public void changeToInvincibleState()
        {
   
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperIdleLeft(mario, content));
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
            mario.setMarioState(new InvincibleJumpingLeft(mario,content));
        }

        public void right()
        {
            mario.setMarioState(new InvincibleWalkingRight(mario,content));
        }

        public void left()
        {
            mario.setMarioState(new InvincibleWalkingLeft(mario,content));
        }
    }
}
