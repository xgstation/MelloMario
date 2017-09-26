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
            sprite = SpriteFactory.Instance.CreateMarioSprite("StandardWalkingLeft", setToStatic);
        }

        public void die()
        {
            mario.State = new Dead(mario);
        }

        public void changeToFireState()
        {
            mario.State = new FireWalkingLeft(mario);
        }

        public void changeToStandardState()
        {
            //nothing here
        }

        public void changeToSuperState()
        {
            mario.State = new SuperWalkingLeft(mario);
        }

        public void down()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void idle()
        {
            mario.State = new StandardIdleLeft(mario);
        }

        public void left()
        {
            //nothing here
        }

        public void right()
        {
            //walk right
            mario.State = new StandardWalkingRight(mario);
        }

        public void up()
        {
            mario.State = new StandardJumpingLeft(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
