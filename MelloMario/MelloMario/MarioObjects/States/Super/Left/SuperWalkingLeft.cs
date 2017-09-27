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
    class SuperWalkingLeft : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public SuperWalkingLeft(Mario newMario)
        {
            mario = newMario;
            setToStatic = false;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperWalkingLeft", setToStatic);
        }

        public void Die()
        {
            mario.State = new Dead(mario);
        }

        public void ChangeToFireState()
        {
            mario.State = new FireWalkingLeft(mario);
        }

        public void ChangeToStandardState()
        {
            mario.PrevWalking = true;
            mario.State = new StandardWalkingLeft(mario);
        }

        public void ChangeToSuperState()
        {
            //nothing here
        }

        public void Down()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void Left()
        {
            //nothing here
        }

        public void Right()
        {
            //walk right
            mario.PrevWalking = false;
            mario.State = new SuperIdleLeft(mario);
        }

        public void Up()
        {
            mario.State = new SuperJumpingLeft(mario);
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
