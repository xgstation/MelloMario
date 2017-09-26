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

        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireWalkingLeft(mario);
        }

        public void ChangeToStandardState()
        {
            //nothing here
        }

        public void ChangeToSuperState()
        {
            mario.State = new SuperWalkingLeft(mario);
        }

        public void Down()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void Idle()
        {
            mario.State = new StandardIdleLeft(mario);
        }

        public void Left()
        {
            //nothing here
        }

        public void Right()
        {
            //walk right
            mario.State = new StandardIdleLeft(mario);
        }

        public void Up()
        {
            mario.State = new StandardJumpingLeft(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
