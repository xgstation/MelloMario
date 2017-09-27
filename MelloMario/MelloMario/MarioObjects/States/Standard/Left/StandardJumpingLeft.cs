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
    class StandardJumpingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public StandardJumpingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("StandardJumpingLeft", setToStatic);


        }

        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireJumpingLeft(mario);
        }

        public void ChangeToStandardState()
        {
            //nothing here
            
        }

        public void ChangeToSuperState()
        {
            mario.State = new SuperJumpingLeft(mario);
        }

        public void Down()
        {
            mario.State = new StandardWalkingLeft(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Left()
        {
            //nothing here
        }

        public void Right()
        {
            //jumping right
            mario.State = new StandardJumpingRight(mario);
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
