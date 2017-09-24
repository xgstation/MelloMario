using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.Sprites
{
    class DestroyingSprite : ISprite
    {
        private const int totalFrame = 5;
        private int currentFrame = 0;
        public int TotalFrame()
        {
            return totalFrame;
        }
        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrame)
            {
                currentFrame = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location) { }
    }
}
