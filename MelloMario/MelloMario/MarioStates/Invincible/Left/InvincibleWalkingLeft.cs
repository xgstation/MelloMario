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
    class InvincibleWalkingLeft : IMarioState
    {
        Mario mario;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        bool setToStatic;
        ContentManager content;

        public InvincibleWalkingLeft(Mario newMario, ContentManager content)
        {
            mario = newMario;
            spriteCreation = new SpriteFactory();
            setToStatic = false;
            sprite = spriteCreation.createSprite("InvincibleWalkingLeft", setToStatic, content);
            this.content = content;
        }

        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireWalkingLeft(mario, content));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardWalkingLeft(mario,content));
        }

        public void changeToInvincibleState()
        {
           
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperWalkingLeft(mario,content));
        }

        public void down()
        {
            mario.setMarioState(new InvincibleCrouchingLeft(mario,content));
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
            mario.setMarioState(new InvincibleIdleLeft(mario,content));
        }

        public void left()
        {
           //nothing here
        }

        public void right()
        {
            //walk right
            mario.setMarioState(new InvincibleWalkingRight(mario,content));
        }

        public void up()
        {
            mario.setMarioState(new InvincibleJumpingLeft(mario,content));
        }

        public void Update()
        {
            
        }
    }
}
