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
    class SuperCrouchingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;

        public SuperCrouchingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingLeft", setToStatic);

        }
        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireCrouchingLeft(mario);
        }

        public void ChangeToStandardState()
        {
        }

        public void ChangeToSuperState()
        {
            //nothing to do here
        }

        public void Down()
        {
            //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }
        public void Idle()
        {
            mario.State = new SuperIdleLeft(mario);
        }
        public void Left()
        {
           //nothing here
        }

        public void Right()
        {
            //right crouching
            mario.State = new SuperCrouchingRight(mario);
        }

        public void Up()
        {
            mario.State = new SuperIdleLeft(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
