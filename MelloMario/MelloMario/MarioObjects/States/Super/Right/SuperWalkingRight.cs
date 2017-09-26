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
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperWalkingRight", setToStatic);
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
            mario.State = new StandardWalkingRight(mario);
        }

        public void ChangeToSuperState()
        {
            //nothing here
        }

        public void Down()
        {
            mario.State = new SuperCrouchingRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }
        public void Idle()
        {
            mario.State = new SuperIdleRight(mario);
        }

        public void Left()
        {
            //walk left
            mario.State = new SuperWalkingLeft(mario);
        }

        public void Right()
        {
            //nothing here
        }

        public void Up()
        {
            mario.State = new SuperJumpingRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
