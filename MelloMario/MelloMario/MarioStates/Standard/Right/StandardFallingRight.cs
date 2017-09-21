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
    class StandardFallingRight : IMarioState
    {
        Mario mario;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        bool setToStatic;
        ContentManager content;
        public StandardFallingRight(Mario mario,ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("StandardFallingRight",setToStatic,content);
        }
        //falling
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireFallingRight(mario,content));
        }

        public void changeToStandardState()
        {
         //nothing to do here
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleFallingRight(mario, content));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperFallingRight(mario, content));
        }

        public void down()
        {
         //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void fall()
        {
            //nothing to do here
        }

        public void idle()
        {
            //nothing to do here
        }

        public void left()
        {
            mario.setMarioState(new StandardFallingLeft(mario,content));
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
