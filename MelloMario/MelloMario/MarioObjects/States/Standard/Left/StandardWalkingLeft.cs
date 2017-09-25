using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.MarioObjects.States
{
    class StandardWalkingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardWalkingLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateSprite("StandardWalkingLeft", setToStatic);
        }

        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireWalkingLeft(mario));
        }

        public void changeToStandardState()
        {
            //nothing here
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperWalkingLeft(mario));
        }

        public void down()
        {
            mario.setMarioState(new StandardCrouchingLeft(mario));
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
            mario.setMarioState(new StandardIdleLeft(mario));
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //walk right
            mario.setMarioState(new StandardWalkingRight(mario));
        }

        public void up()
        {
            mario.setMarioState(new StandardJumpingLeft(mario));
        }

        public void Update(GameTime game)
        {
            
        }
    }
}
