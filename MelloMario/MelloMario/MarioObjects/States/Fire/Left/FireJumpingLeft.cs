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
    class FireJumpingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public FireJumpingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireJumpingLeft", setToStatic);


        }

        public void Die()
        {
            mario.State=new Dead(mario);
        }

        public void ChangeToFireState()
        {
            //nothing here
        }

        public void ChangeToStandardState()
        {
            mario.State=new StandardJumpingLeft(mario);
        }

        public void ChangeToSuperState()
        {
            mario.State=new SuperJumpingLeft(mario);
        }

        public void Down()
        {
            mario.State=new FireIdleLeft(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void Idle()
        {
            mario.State=new FireIdleLeft(mario);
        }

        public void Left()
        {
            //nothing to do here
        }

        public void Right()
        {
            //right jump
            mario.State=new FireJumpingRight(mario);
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
