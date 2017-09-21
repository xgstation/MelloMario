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
    class StandardIdleRight : IMarioState
    {
        Mario mario;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        bool setToStatic;
        ContentManager content;

        public StandardIdleRight(Mario newMario,ContentManager content)
        {
            mario = newMario;
            spriteCreation = new SpriteFactory();
            setToStatic = true;
            sprite = spriteCreation.createSprite("StandardIdleRight",setToStatic,content);
            this.content = content;
        }
        public void down() {
            mario.setMarioState(new StandardCrouchingRight(mario, content));
        }
        public void changeToFireState()
        {
            mario.setMarioState(new FireIdleRight(mario, content));
        }
        public void changeToSuperState()
        {
            mario.setMarioState(new SuperIdleRight(mario, content));   
        }
        public void changeToStandardState()
        {
            //nothing to do here
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }
        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleIdleRight(mario, content));
        }
        public void Update()
        {
            //Nothing to do here
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            
            sprite.Draw(spriteBatch,location);
            
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
            mario.setMarioState(new StandardJumpingRight(mario,content));
        }

        public void right()
        {
            mario.setMarioState(new StandardWalkingRight(mario,content));
        }

        public void left()
        {
            mario.setMarioState(new StandardWalkingLeft(mario,content));
        }
    }
}
