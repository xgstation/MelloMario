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
    class FireFallingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setStatic;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        public FireFallingLeft(Mario mario,ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("FireFallingLeft",setStatic,content);


        }
        public void die()
        {
            mario.setMarioState(new Dead(mario,content));   
        }

        public void changeToFireState()
        {
            //nothing here
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardFallingLeft(mario,content));
            
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleFallingLeft(mario, content));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperFallingLeft(mario,content));
        }

        public void down()
        {
            //nothing here
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
            //nothing here
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            mario.setMarioState(new FireFallingRight(mario,content));
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
