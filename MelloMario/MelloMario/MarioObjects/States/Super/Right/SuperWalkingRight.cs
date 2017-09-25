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
    class SuperWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public SuperWalkingRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateSprite("SuperWalkingRight", setToStatic);
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
            mario.setMarioState(new StandardWalkingRight(mario));
        }

        public void changeToSuperState()
        {
            //nothing here
        }

        public void down()
        {
            mario.setMarioState(new SuperCrouchingRight(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }
        public void idle()
        {
            mario.setMarioState(new SuperIdleRight(mario));
        }

        public void left()
        {
            //walk left
            mario.setMarioState(new SuperWalkingLeft(mario));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.setMarioState(new SuperJumpingRight(mario));
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
