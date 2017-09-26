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
    class FireIdleLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;
       
        public FireIdleLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("FireIdleLeft", setToStatic);
        }
        public void Down()
        {
            mario.State = new FireCrouchingLeft(mario);
        }
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
            mario.State = new StandardIdleLeft(mario);
        }

        public void ChangeToSuperState()
        {
            mario.State = new SuperIdleLeft(mario);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
         
            sprite.Draw(spriteBatch,location);
           
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }

        public void Idle()
        {
            //nothing to do here
        }
        public void Up()
        {
            mario.State = new FireJumpingLeft(mario);
        }

        public void Right()
        {
            mario.State = new FireWalkingRight(mario);
        }

        public void Left()
        {
            mario.State = new FireWalkingLeft(mario);
        }
    }
}
