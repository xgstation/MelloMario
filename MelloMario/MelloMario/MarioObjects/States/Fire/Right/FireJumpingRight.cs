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
    class FireJumpingRight : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public FireJumpingRight(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireJumpingRight", setToStatic);
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
            mario.State = new StandardJumpingRight(mario);
        }
        public void ChangeToSuperState()
        {
            mario.State = new SuperJumpingRight(mario);
        }

        public void Down()
        {
            mario.State = new FireIdleRight(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void Idle()
        {
            mario.State = new FireIdleRight(mario);
        }

        public void Left()
        {
            mario.State = new FireIdleLeft(mario);
        }

        public void Right()
        {
            //nothing to do here
        }

        public void Up()
        {
            //nothing to do here
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
