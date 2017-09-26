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
    class StandardIdleLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public StandardIdleLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("StandardIdleLeft",setToStatic);
        }
        public void Down() {
            //nothing here
        }
        public void ChangeToFireState()
        {
            mario.State = new FireIdleLeft(mario);
        }
        public void ChangeToSuperState()
        {
            mario.State = new SuperIdleLeft(mario);   
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

        public void Idle()
        {
            //nothing to do here
        }

        public void Up()
        {
            mario.State = new StandardJumpingLeft(mario);
        }

        public void Right()
        {
            mario.State = new StandardWalkingRight(mario);
        }

        public void Left()
        {
            mario.State = new StandardWalkingLeft(mario);
        }
    }
}
