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
    class FireJumpingLeft : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setStatic;
        ISpriteFactory spriteCreation;
        ISprite sprite;
        public FireJumpingLeft(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setStatic = true;
            spriteCreation = new SpriteFactory();
            sprite = spriteCreation.createSprite("FireJumpingLeft", setStatic, content);


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
            mario.setMarioState(new StandardJumpingLeft(mario,content));
        }

        public void changeToInvincibleState()
        {
            mario.setMarioState(new InvincibleJumpingLeft(mario, content));
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperJumpingLeft(mario,content));
        }

        public void down()
        {
            mario.setMarioState(new FireIdleLeft(mario,content));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            mario.setMarioState(new FireFallingLeft(mario,content));
        }

        public void idle()
        {
            mario.setMarioState(new FireIdleLeft(mario,content));
        }

        public void left()
        {
            //nothing to do here
        }

        public void right()
        {
            //right jump
            mario.setMarioState(new FireJumpingRight(mario,content));
        }

        public void up()
        {
            //nothing to do here
        }

        public void Update()
        {
            
        }
    }
}
