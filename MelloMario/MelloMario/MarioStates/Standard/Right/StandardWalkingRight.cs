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
    class StandardWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardWalkingRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.createSprite("StandardWalkingRight", setToStatic);

        }

        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireWalkingRight(mario));
        }

        public void changeToStandardState()
        {
            //nothing here
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperWalkingRight(mario));
        }

        public void down()
        {
            mario.setMarioState(new StandardCrouchingRight(mario));
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
            mario.setMarioState(new StandardIdleRight(mario));
        }

        public void jump()
        {
            mario.setMarioState(new StandardJumpingRight(mario));
        }

        public void left()
        {
            mario.setMarioState(new StandardJumpingLeft(mario));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.setMarioState(new StandardJumpingRight(mario));
        }

        public void Update(GameTime game)
        {
            
        }
    }
}
