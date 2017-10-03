using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.PowerUpStates
{
    class SuperIdleLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        
        public SuperIdleLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperIdleLeft",setToStatic);

        }
        public void Down()
        {
            mario.State = new SuperCrouchingLeft(mario);
        }
        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireIdleLeft(mario); 
        }

        public void ChangeToStandardState()
        {
            mario.State = new StandardIdleLeft(mario);
        }

        public void ChangeToSuperState()
        {
            //nothing to do here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }

        public void Up()
        {
            mario.State = new SuperJumpingLeft(mario);
        }

        public void Right()
        {
            mario.State = new SuperIdleRight(mario);
        }

        public void Left()
        {
            mario.PrevWalking = true;
            mario.State = new SuperWalkingLeft(mario);
        }
    }
}
