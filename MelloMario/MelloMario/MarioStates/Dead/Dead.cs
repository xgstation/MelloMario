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
    class Dead : IMarioState
    {
        Mario mario;
        ContentManager content;
        ISprite sprite;
        bool setToStatic;
        public Dead(Mario mario, ContentManager content)
        {
            this.mario = mario;
            this.content = content;
            setToStatic = true;
            sprite = SpriteFactory.Instance.createSprite("Dead",setToStatic,content);


        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch,location);
        }
        public void die() { }
        public void changeToFireState() { }
        public void changeToInvincibleState() { }
        public void changeToStandardState() { }
        public void changeToSuperState() { }
        public void down() { }
        public void fall() { }
        public void idle() { }
        public void left(){ }
        public void right() { }
        public void up() { }
        public void Update() { }      
    }
}
