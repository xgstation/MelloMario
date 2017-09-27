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
    class SuperCrouchingRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;

        public SuperCrouchingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite =SpriteFactory.Instance.CreateMarioSprite("SuperCrouchingRight", setToStatic);

        }
        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireCrouchingRight(mario);
        }

        public void ChangeToStandardState()
        {
            //nothing here
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

        public void Left()
        {
          
        }

        public void Right()
        {
            //nothing here
        }

        public void Up()
        {
            mario.State = new SuperIdleRight(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
