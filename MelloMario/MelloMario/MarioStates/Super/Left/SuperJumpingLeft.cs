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
    class SuperJumpingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setStatic;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        public SuperJumpingLeft(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("SuperJumpingLeft", setStatic, content);


        }

        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingLeft(mario,content));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardJumpingLeft(mario,content));
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleJumpingLeft(mario, content));

        }

        public void changeToSuperState()
        {
         //nothing here  
        }

        public void down()
        {
            mario.setMarioState(new SuperIdleLeft(mario,content));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            mario.setMarioState(new SuperFallingLeft(mario,content));
        }

        public void idle()
        {
            mario.setMarioState(new SuperIdleLeft(mario,content));   
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //jump right
            mario.setMarioState(new SuperJumpingRight(mario,content));
        }

        public void up()
        {
           //nothing here
        }

        public void Update()
        {
            
        }
    }
}
