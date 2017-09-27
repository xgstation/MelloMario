using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario.MarioObjects.States
{
    class SuperJumpingRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public SuperJumpingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperJumpingRight", setToStatic);


        }

        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireJumpingRight(mario);
        }

        public void ChangeToStandardState()
        {
            mario.State = new StandardJumpingRight(mario);
        }

        public void ChangeToSuperState()
        {
         //nothing here  
        }

        public void Down()
        {
            mario.State = new SuperWalkingRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void Left()
        {
            //jump left
            mario.State = new SuperJumpingLeft(mario);
        }

        public void Right()
        {
            //nothing here
        }

        public void Up()
        {
           //nothing here
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
