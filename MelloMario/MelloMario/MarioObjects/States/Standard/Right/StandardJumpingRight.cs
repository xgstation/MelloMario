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
    class StandardJumpingRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public StandardJumpingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("StandardJumpingRight", setToStatic);

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
            //nothing here
            
        }

        public void ChangeToSuperState()
        {
            mario.State = new SuperJumpingRight(mario);
        }

        public void Down()
        {
            mario.State = new StandardWalkingRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Left()
        {
            mario.State = new StandardJumpingLeft(mario);
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
