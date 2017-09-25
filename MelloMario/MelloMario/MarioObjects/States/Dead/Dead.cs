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
    class Dead : IMarioState
    {
        Mario mario;
        ISprite sprite;
        bool setToStatic;

        public Dead(Mario mario)
        {
            this.mario = mario;
            setToStatic = true;
            sprite = SpriteFactory.Instance.CreateMarioSprite("Dead",setToStatic);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }
        public void die() { }
        public void changeToFireState() { }
        public void changeToInvincibleState() { }
        public void changeToStandardState() { }
        public void changeToSuperState() { }
        public void down() { }
        public void idle() { }
        public void left(){ }
        public void right() { }
        public void up() { }
        public void Update(GameTime game) { sprite.Update(game); }      
    }
}
