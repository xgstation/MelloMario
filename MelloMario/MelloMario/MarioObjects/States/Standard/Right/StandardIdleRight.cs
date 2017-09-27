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
    class StandardIdleRight : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardIdleRight(Mario newMario)
        {
            mario = newMario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("StandardIdleRight", setToStatic);
        }
        public void Down() {
            //nothing here for standard
        }
        public void ChangeToFireState()
        {
            mario.State = new FireIdleRight(mario);
        }
        public void ChangeToSuperState()
        {
            mario.State = new SuperIdleRight(mario);   
        }
        public void ChangeToStandardState()
        {
            //nothing to do here
        }
        public void Die()
        {
            mario.State = new Dead(mario);
        }
        public void Update(GameTime game)
        {
            //Nothing to do here
            sprite.Update(game);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            
            sprite.Draw(spriteBatch,location);
            
        }

  
        public void Up()
        {
            mario.State = new StandardJumpingRight(mario);
        }

        public void Right()
        {
            mario.State = new StandardWalkingRight(mario);
            mario.PrevWalking = true;
        }

        public void Left()
        {
            mario.State = new StandardIdleLeft(mario);
        }
    }
}
