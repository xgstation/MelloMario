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
    class SuperFallingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setToStatic;
        ISpriteFactory spriteCreation;
        ISprite sprite;

        public SuperFallingLeft(Mario mario,ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("SuperFallingLeft",setToStatic,content);
        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireFallingLeft(mario,content));
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

        }

        public void down()
        {
            //nothin here
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
            //right falling
            mario.setMarioState(new SuperFallingRight(mario,content));
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
