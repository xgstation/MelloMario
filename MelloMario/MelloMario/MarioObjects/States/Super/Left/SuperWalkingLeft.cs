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
    class SuperWalkingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public SuperWalkingLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperWalkingLeft", setToStatic);
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
            mario.setMarioState(new StandardWalkingLeft(mario));
        }

        public void changeToSuperState()
        {
            //nothing here
        }

        public void down()
        {
            mario.setMarioState(new SuperCrouchingLeft(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void idle()
        {
            mario.setMarioState(new SuperIdleLeft(mario));
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //walk right
            mario.setMarioState(new SuperWalkingRight(mario));
        }

        public void up()
        {
            mario.setMarioState(new SuperJumpingLeft(mario));
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
