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
    class SuperJumpingRight : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setStatic;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        public SuperJumpingRight(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("SuperJumpingRight", setStatic, content);


        }

        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingRight(mario,content));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardJumpingRight(mario,content));
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleJumpingRight(mario,content));
        }

        public void changeToSuperState()
        {
         //nothing here  
        }

        public void down()
        {
            mario.setMarioState(new SuperIdleRight(mario,content));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            mario.setMarioState(new SuperFallingRight(mario,content));
        }

        public void idle()
        {
            mario.setMarioState(new SuperIdleRight(mario,content));   
        }

        public void left()
        {
            //jump left
            mario.setMarioState(new SuperJumpingLeft(mario,content));
        }

        public void right()
        {
            //nothing here
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
