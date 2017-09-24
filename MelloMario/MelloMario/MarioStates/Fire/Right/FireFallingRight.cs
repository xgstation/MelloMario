using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    class FireFallingRight : IMarioState
    {
        Mario mario;
        ContentManager content;
        bool setStatic;
        ISprite sprite;
        public FireFallingRight(Mario mario,ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setStatic = true;
            sprite = SpriteFactory.Instance.createSprite("FireFallingRight",setStatic,content);


        }
        public void die()
        {
            mario.setMarioState(new Dead(mario, content));
        }

        public void changeToFireState()
        {
            //nothing here
        }

        public void changeToStandardState()
        {
            mario.setMarioState(new StandardFallingRight(mario,content));
            
        }

        public void changeToSuperState()
        {
            mario.setMarioState(new SuperFallingRight(mario,content));
        }

        public void down()
        {
            //nothing here
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }

        public void fall()
        {
            //nothing here
        }

        public void idle()
        {
            //nothing here
        }

        public void left()
        {
            mario.setMarioState(new FireFallingLeft(mario,content));
        }

        public void right()
        {
            //nothing here
        }

        public void up()
        {
            //nothing here
        }

        public void Update()
        {
        }
    }
}
