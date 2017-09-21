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
    class InvincibleCrouchingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        ISpriteFactory spriteCreation;
        bool setToStatic;
        ISprite sprite;
        public InvincibleCrouchingLeft(Mario mario,ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("InvincibleCrouchingLeft", setToStatic, content);
        }
        public void down()
        {
            //nothing to do here
        }
        //crouching
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireCrouchingLeft(mario,content));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardCrouchingLeft(mario, content));
        }

        public void changeToInvincibleState()
        {
            
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperCrouchingLeft(mario, content));           
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
            mario.setMarioState(new InvincibleIdleLeft(mario, content));
        }

        public void fall()
        {
            //nothing to do here           
        }

        public void up()
        {
            mario.setMarioState(new InvincibleIdleLeft(mario,content));
        }

        public void right()
        {
            mario.setMarioState(new InvincibleCrouchingRight(mario,content));
        }

        public void left()
        {
            //left crouch
        }
    }
}
