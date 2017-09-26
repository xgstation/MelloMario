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
        public void Die() { }
        public void ChangeToFireState() { }
        public void ChangeToInvincibleState() { }
        public void ChangeToStandardState() { }
        public void ChangeToSuperState() { }
        public void Down() { }
        public void Idle() { }
        public void Left(){ }
        public void Right() { }
        public void Up() { }
        public void Update(GameTime game) { sprite.Update(game); }      
    }
}
