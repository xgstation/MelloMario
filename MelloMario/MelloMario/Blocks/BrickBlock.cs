using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Blocks
{
    class BrickBlock : IBlock
    {
        public enum State
        {
            Normal, Hidden, Destroy
        }
        public State state { get; set; }
        private ISprite sprite;
        private int Frame = 0;
        public BrickBlock (State state)
        {
            this.state = state;
            switch(this.state)
            {
                case State.Normal:
                    {
                        sprite = new Sprites.BrickSprite();
                        break;
                    }
                case State.Hidden:
                    {
                        sprite = null;
                        break;
                    }
            }
        }
        
        public void Update()
        {
            if (state == State.Destroy)
            {
                if (Frame == 0)
                {
                    sprite = new Sprites.DestroyingSprite();
                } else
                {
                    Frame++;
                    if (Frame == sprite.TotalFrame())
                    {
                        sprite = null;
                    }
                }
            }
            sprite.Update();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }
    }
}
