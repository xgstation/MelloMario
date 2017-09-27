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
    class FireWalkingRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public FireWalkingRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireWalkingRight", setToStatic);
        }

        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            //nothing here
        }

        public void ChangeToStandardState()
        {
            mario.State = new StandardWalkingRight(mario);
        }

        public void ChangeToSuperState()
        {
            mario.State = new SuperWalkingRight(mario);
        }

        public void Down()
        {
            mario.State = new FireCrouchingRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }
        public void Left()
        {
            mario.State = new FireIdleRight(mario);
        }

        public void Right()
        {
           //nothing here
        }

        public void Up()
        {
            mario.State = new FireJumpingRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
