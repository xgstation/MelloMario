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

        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireWalkingRight(mario);
        }

        public void ChangeToStandardState()
        {
            //nothing here
        }

        public void ChangeToSuperState()
        {
            mario.State = new SuperWalkingRight(mario);
        }

        public void Down()
        {
           //nothing here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void Left()
        {
            mario.State = new StandardIdleRight(mario);
        }

        public void Right()
        {
            //nothing here
        }

        public void Up()
        {
            mario.State = new StandardJumpingRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
