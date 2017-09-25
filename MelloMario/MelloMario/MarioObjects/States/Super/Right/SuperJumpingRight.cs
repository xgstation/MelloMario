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
        bool setStatic;
        ISprite sprite;
        public SuperJumpingRight(Mario mario)
        {
            this.mario = mario;
            setStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("SuperJumpingRight", setStatic);


        }

        public void die()
        {
            mario.setMarioState(new Dead(mario));
        }

        public void changeToFireState()
        {
            mario.setMarioState(new FireJumpingRight(mario));
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardJumpingRight(mario));
        }

        public void changeToSuperState()
        {
         //nothing here  
        }

        public void down()
        {
            mario.setMarioState(new SuperIdleRight(mario));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void idle()
        {
            mario.setMarioState(new SuperIdleRight(mario));   
        }

        public void left()
        {
            //jump left
            mario.setMarioState(new SuperJumpingLeft(mario));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
           //nothing here
        }

        public void Update(GameTime game)
        {
            sprite.Update(game);
        }
    }
}
