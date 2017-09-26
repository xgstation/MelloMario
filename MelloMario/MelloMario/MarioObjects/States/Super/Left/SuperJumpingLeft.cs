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
    class SuperJumpingLeft : IMarioState
    {
        Mario mario;
        bool setToStatic;
        ISprite sprite;
        public SuperJumpingLeft(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperJumpingLeft", setToStatic);


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
            mario.State = new StandardJumpingLeft(mario);
        }


        public void ChangeToSuperState()
        {
         //nothing here  
        }

        public void Down()
        {
            mario.State = new SuperIdleLeft(mario);
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
            //jump right
            mario.State = new SuperJumpingRight(mario);
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
