using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.MarioObjects.MovementStates
{
    class FireCrouchingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public FireCrouchingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireCrouchingLeft", setToStatic);
            
        }
        public void Down()
        {
            //nothing to do here
        }
        //crouching
        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            //nothing to do here
        }

        public void ChangeToStandardState()
        {
            //nothing here
        }

        public void ChangeToSuperState()
        {
            mario.State = new SuperCrouchingLeft(mario);           
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
            mario.State = new FireIdleLeft(mario);
        }

        public void Right()
        {
            
        }

        public void Left()
        {
            //left crouch
        }
    }
}
