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
    class StandardWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardWalkingRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("StandardWalkingRight", setToStatic);

        }

        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireWalkingRight(mario);
        }

        public void changeToStandardState()
        {
            //nothing here
        }

        public void changeToSuperState()
        {
            mario.State = new SuperWalkingRight(mario);
        }

        public void down()
        {
           //nothing here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void idle()
        {
            mario.State = new StandardIdleRight(mario);
        }

        public void left()
        {
            mario.State = new StandardWalkingLeft(mario);
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            mario.State = new StandardJumpingRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
